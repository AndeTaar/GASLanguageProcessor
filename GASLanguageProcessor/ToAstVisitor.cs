using System;
using System.Collections.Generic;
using System.Linq;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor;

public class ToAstVisitor : GASBaseVisitor<AstNode> {
    public override AstNode VisitProgram ( GASParser.ProgramContext context )
    {
        var lines =  context.children
            .Select ( line => line.Accept(this)).ToList();

        return ToCompound(lines);
    }

    public override AstNode VisitCanvas(GASParser.CanvasContext context)
    {
        var width = context.expression()[0].Accept(this) as Expression;

        var height = context.expression()[1].Accept(this) as Expression;

        var backgroundColor = context.expression()[2].Accept(this) as Expression;

        return new Canvas(width, height, backgroundColor);
    }

    public override AstNode VisitIfStatement(GASParser.IfStatementContext context)
    {
        Expression condition = context.expression().Accept(this) as Expression;

        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        var ifBody = ToCompound(statements);

        var @else = context.elseStatement()?.Accept(this) as Statement;

        return new If(condition, ifBody, @else) {LineNumber = context.Start.Line};
    }

    public override AstNode VisitElseStatement(GASParser.ElseStatementContext context)
    {
        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        var elseBody = ToCompound(statements);

        var elseIf = context.ifStatement()?.Accept(this) as If;

        return elseIf ?? elseBody;
    }

    public override AstNode VisitWhileStatement(GASParser.WhileStatementContext context)
    {
        var condition = context.expression().Accept(this) as Expression;

        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        var whileBody = ToCompound(statements) as Statement;

        return new While(condition, whileBody);
    }

    public override AstNode VisitForStatement(GASParser.ForStatementContext context)
    {
        var declaration = context.declaration()?.Accept(this) as Declaration;
        var assignment = context.assignment()[0].Accept(this) as Assignment;

        Assignment assignment2 = null;

        if (declaration == null)
        {
            assignment2 = context.assignment()[1].Accept(this) as Assignment;
        }

        var condition = context.expression().Accept(this) as Expression;

        var allStatements = context.statement().Select(s => s.Accept(this)).ToList();
        var statements = ToCompound(allStatements);

        if (declaration != null)
        {
            return new For(declaration, condition, assignment, statements);
        }

        return new For(assignment, condition, assignment2, statements){ LineNumber = context.Start.Line };
    }

    public override AstNode VisitAssignment(GASParser.AssignmentContext context)
    {
        var allIdentifiers = context.IDENTIFIER().Select(id => new Identifier(id.GetText()) {LineNumber = context.Start.Line}).ToList();
        Identifier identifier = ToCompoundIdentifier(allIdentifiers);
        string op = context.GetChild(1).GetText();
        Expression value = context.expression().Accept(this) as Expression;

        return new Assignment(identifier, value, op) {LineNumber = context.Start.Line};
    }

    public override AstNode VisitGroupTerm(GASParser.GroupTermContext context)
    {
        var expression = context.expression().Accept(this) as Expression;

        Statement? statements = ToCompound(context.statement()?.Select(c => c.Accept(this)).ToList());

        return new Group(expression, statements);
    }

    public override AstNode VisitDeclaration(GASParser.DeclarationContext context)
    {
        var type = context.type()?.Accept(this) as Type;
        Identifier identifier = new Identifier(context.IDENTIFIER().GetText()) {LineNumber = context.Start.Line};

        Expression value = context.expression()?.Accept(this) as Expression;

        return new Declaration(type, identifier, value) {LineNumber = context.Start.Line};
    }

    public override AstNode VisitCollectionDeclaration(GASParser.CollectionDeclarationContext context)
    {
        var type = context.collectionType()?.Accept(this) as Type;
        Identifier identifier = new Identifier(context.IDENTIFIER().GetText());

        var expressions = context.expression()?.Accept(this) as Expression;

        return new CollectionDeclaration(type, identifier, expressions) {LineNumber = context.Start.Line};
    }

    public override AstNode VisitType(GASParser.TypeContext context)
    {
        return new Type(context.GetText()){LineNumber = context.Start.Line};
    }

    public override AstNode VisitCollectionType(GASParser.CollectionTypeContext context)
    {
        return new Type(context.GetText()){LineNumber = context.Start.Line};
    }

    public override AstNode VisitSimpleStatement(GASParser.SimpleStatementContext context)
    {
        return context.GetChild(0)?.Accept(this);
    }

    public override AstNode VisitExpression(GASParser.ExpressionContext context)
    {
        if (context.children.Count == 1)
        {
            return base.VisitExpression(context);
        }

        var equalExpression = context.equalityExpression().Select(mu => mu.Accept(this) as Expression).ToList();
        var expression = context.expression()?.Accept(this) as Expression;

        return new BinaryOp(equalExpression[0], context.GetChild(1).GetText(), expression ?? equalExpression[1]) {LineNumber = context.Start.Line};
    }

    public override AstNode VisitEqualityExpression(GASParser.EqualityExpressionContext context)
    {
        if (context.children.Count == 1)
        {
            return base.VisitEqualityExpression(context);
        }

        var left = context.GetChild(0).Accept(this) as Expression;

        var right = context.GetChild(2).Accept(this) as Expression;

        return new BinaryOp(left, context.GetChild(1).GetText(), right);
    }

    public override AstNode VisitReturnStatement(GASParser.ReturnStatementContext context)
    {
        Expression expression = context?.expression().Accept(this) as Expression;
        return new Return(expression);
    }

    public override AstNode VisitFunctionCall(GASParser.FunctionCallContext context)
    {
        var allIdentifiers = context.IDENTIFIER().Select(id => new Identifier(id.GetText()) {LineNumber = context.Start.Line}).ToList();
        var identifier = ToCompoundIdentifier(allIdentifiers);
        var arguments = context.expression().ToList().Select(expr => expr.Accept(this) as Expression).ToList();
        if (context.Parent is GASParser.ExpressionContext || context.Parent is GASParser.TermContext)
        {
            return new FunctionCallTerm(identifier, arguments) { LineNumber = context.Start.Line };
        }
        return new FunctionCallStatement(identifier, arguments) {LineNumber = context.Start.Line};
    }

    public override AstNode VisitFunctionDeclaration(GASParser.FunctionDeclarationContext context)
    {
        var returnType = context.type()[0].Accept(this) as Type;
        var identifier = new Identifier(context.IDENTIFIER()[0].GetText());

        var types = context.type().Skip(1).ToList();
        var identifiers = context.IDENTIFIER().Skip(1).ToList();

        var parameters = types.Zip(identifiers, (typeNode, identifierNode) =>
        {
            var type = typeNode.Accept(this) as Type;
            var identif = new Identifier(identifierNode.GetText()) {LineNumber = context.Start.Line};
            return new Declaration(type, identif, null) {LineNumber = context.Start.Line};
        }).ToList();

        var statements = context.statement().Select(stmt => stmt.Accept(this)).ToList();
        var body = ToCompound(statements);
        return new FunctionDeclaration(identifier, parameters, body, returnType) {LineNumber = context.Start.Line};
    }

    public override AstNode VisitRelationExpression(GASParser.RelationExpressionContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitRelationExpression(context);
        }

        var left = context.binaryExpression()[0].Accept(this) as Expression;

        var right = context.binaryExpression()[1].Accept(this) as Expression;

        return new BinaryOp(left, context.GetChild(1).GetText(), right) {LineNumber = context.Start.Line};
    }

    public override AstNode VisitBinaryExpression(GASParser.BinaryExpressionContext context)
    {
        if (context.children.Count == 1)
        {
            return base.VisitBinaryExpression(context);
        }

        var multExpressions = context.multExpression().Select(mu => mu.Accept(this) as Expression).ToList();
        var binaryExpression = context.binaryExpression()?.Accept(this) as Expression;

        return new BinaryOp(multExpressions[0], context.GetChild(1).GetText(), binaryExpression ?? multExpressions[1]) {LineNumber = context.Start.Line};
    }

    public override AstNode VisitTerm(GASParser.TermContext context)
    {
        if (context.NUM() != null)
        {
            return new Number(context.NUM().GetText()) {LineNumber = context.Start.Line};
        }
        else if (context.functionCall() != null)
        {
            return VisitFunctionCall(context.functionCall());
        }
        else if (context.ALLSTRINGS() != null)
        {
            return new String(context.ALLSTRINGS().GetText()) {LineNumber = context.Start.Line};
        }
        else if (context.expression() != null)
        {
            return VisitExpression(context.expression());
        }
        else if (context.GetText() == "true" || context.GetText() == "false")
        {
            return new Boolean(context.GetText()) {LineNumber = context.Start.Line};
        }
        else if (context.GetText() == "null")
        {
            return new Null();
        }else if(context.groupTerm() != null)
        {
            return VisitGroupTerm(context.groupTerm());
        }else if(context.listTerm() != null)
        {
            return VisitListTerm(context.listTerm());
        }
        else if (context.IDENTIFIER() != null)
        {
            var allIdentifiers = context.IDENTIFIER().Select(id => new Identifier(id.GetText()) {LineNumber = context.Start.Line}).ToList();
            return ToCompoundIdentifier(allIdentifiers);
        }
        else
        {
            throw new NotSupportedException($"Term type not supported: {context.GetText()}");
        }
    }

    public override AstNode VisitListTerm(GASParser.ListTermContext context)
    {
        var expressions = context.expression()?.Select(expr => expr.Accept(this) as Expression).ToList();
        return new List(expressions) {LineNumber = context.Start.Line};
    }

    public override AstNode VisitMultExpression(GASParser.MultExpressionContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitMultExpression(context);
        }

        var notExpressions = context.unaryExpression().Select(ne => ne.Accept(this) as Expression).ToList();
        var multExpression = context.multExpression()?.Accept(this) as Expression;

        return new BinaryOp(notExpressions[0], context.GetChild(1).GetText(), multExpression ?? notExpressions[1]) {LineNumber = context.Start.Line};
    }

    public override AstNode VisitUnaryExpression(GASParser.UnaryExpressionContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitUnaryExpression(context);
        }

        var expression = context.listAccessExpression().Accept(this) as Expression;

        return new UnaryOp(context.GetChild(0).GetText(), expression);
    }

    private static Identifier ToCompoundIdentifier(List<Identifier> identifiers)
    {
        if (identifiers.Count == 0)
        {
            return null!;
        }

        if (identifiers.Count == 1)
        {
            return identifiers[0];
        }

        return new Identifier(identifiers[0].Name, ToCompoundIdentifier(identifiers.Skip(1).ToList())) {LineNumber = identifiers[0].LineNumber};
    }

    private static Statement ToCompound(List<AstNode> lines)
    {
        if (lines.Count == 0)
        {
            return null!;
        }

        if(lines.Count == 1)
        {
            return lines[0] as Statement;
        }

        if (lines[0] is Compound compound)
        {
            return new Compound(compound.Statement1,
                new Compound(compound.Statement2, ToCompound(lines.Skip(1).ToList())));
        }

        return new Compound(lines[0] as Statement, ToCompound(lines.Skip(1).ToList()));
        }
}
