using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using Expression = GASLanguageProcessor.AST.Expressions.Expression;
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

        return new Canvas(width, height, backgroundColor) {LineNum = context.Start.Line};
    }

    public override AstNode VisitIfStatement(GASParser.IfStatementContext context)
    {
        Expression condition = context.expression().Accept(this) as Expression;

        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        var ifBody = ToCompound(statements);

        var @else = context.elseStatement()?.Accept(this) as Statement;

        return new If(condition, ifBody, @else) {LineNum = context.Start.Line};
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
        var assignments = context.assignment().Select(a => a.Accept(this) as Assignment).ToList();
        var increment = context.increment()?.Accept(this) as Increment;

        var condition = context.expression().Accept(this) as Expression;

        var allStatements = context.statement().Select(s => s.Accept(this)).ToList();
        var statements = ToCompound(allStatements);

        if (declaration != null)
        {
            if(increment != null)
            {
                return new For(declaration, condition, increment, statements);
            }

            return new For(declaration, condition, assignments[0], statements);
        }

        if(increment != null)
        {
            return new For(assignments[0], condition, increment, statements);
        }

        return new For(assignments[0], condition, assignments[1], statements){ LineNum = context.Start.Line };
    }

    public override AstNode VisitAssignment(GASParser.AssignmentContext context)
    {
        var identifier = new Identifier(context.IDENTIFIER().GetText()) {LineNum = context.Start.Line};
        string op = context.GetChild(1).GetText();
        Expression value = context.expression().Accept(this) as Expression;

        return new Assignment(identifier, value, op) {LineNum = context.Start.Line};
    }

    public override AstNode VisitIncrement(GASParser.IncrementContext context)
    {
        var identifier = new Identifier(context.IDENTIFIER().GetText()) {LineNum = context.Start.Line};
        string op = context.GetChild(1).GetText();

        return new Increment(identifier, op) {LineNum = context.Start.Line};
    }

    public override Group VisitGroupTerm(GASParser.GroupTermContext context)
    {
        var expression = context.expression().Accept(this) as Expression;

        Statement? statements = ToCompound(context.statement()?.Select(c => c.Accept(this)).ToList());

        return new Group(expression, statements);
    }

    public override AstNode VisitDeclaration(GASParser.DeclarationContext context)
    {
        var type = context.type()?.Accept(this) as Type;

        if (type == null)
        {
            type = context.collectionType().Accept(this) as Type;
        }

        Identifier identifier = new Identifier(context.IDENTIFIER().GetText()) {LineNum = context.Start.Line};

        Expression value = context.expression()?.Accept(this) as Expression;

        return new Declaration(type, identifier, value) {LineNum = context.Start.Line};
    }

    public override AstNode VisitType(GASParser.TypeContext context)
    {
        return new Type(context.GetText()){LineNum = context.Start.Line};
    }

    public override AstNode VisitCollectionType(GASParser.CollectionTypeContext context)
    {
        return new Type(context.GetText()){LineNum = context.Start.Line};
    }

    public override AstNode VisitSimpleStatement(GASParser.SimpleStatementContext context)
    {
        return context.GetChild(0)?.Accept(this);
    }

    public override Expression VisitExpression(GASParser.ExpressionContext context)
    {
        var equalExpressions = context.equalityExpression().Select(mu => mu.Accept(this) as Expression).ToList();
        var left = equalExpressions[0] as Expression;
        var operatorIndex = 1;
        for (int i = 1; i < equalExpressions.Count; i++)
        {
            left = new BinaryOp(left, context.GetChild(operatorIndex).GetText(), equalExpressions[i]) {LineNum = context.Start.Line};
            operatorIndex += 2;
        }
        
        return left;
    }

    public override Expression VisitEqualityExpression(GASParser.EqualityExpressionContext context)
    {
        var relationExpressions = context.relationExpression().Select(mu => mu.Accept(this) as Expression).ToList();
        var left = relationExpressions[0] as Expression;
        var operatorIndex = 1;
        for (int i = 1; i < relationExpressions.Count; i++)
        {
            left = new BinaryOp(left, context.GetChild(operatorIndex).GetText(), relationExpressions[i]) {LineNum = context.Start.Line};
            operatorIndex += 2;
        }
        
        return left;
    }

    public override AstNode VisitReturnStatement(GASParser.ReturnStatementContext context)
    {
        Expression expression = context?.expression().Accept(this) as Expression;
        return new Return(expression);
    }
    
    public override FunctionCallStatement VisitFunctionCallStatement(GASParser.FunctionCallStatementContext context)
    {
        var identifier = new Identifier(context.IDENTIFIER().GetText());
        var arguments = context.expression().ToList().Select(expr => expr.Accept(this) as Expression).ToList();
        return new FunctionCallStatement(identifier, arguments) {LineNum = context.Start.Line};
    }
    
    public override FunctionCallTerm VisitFunctionCallTerm(GASParser.FunctionCallTermContext context)
    {
        var identifier = new Identifier(context.IDENTIFIER().GetText());
        var arguments = context.expression().ToList().Select(expr => expr.Accept(this) as Expression).ToList();
        return new FunctionCallTerm(identifier, arguments) {LineNum = context.Start.Line};
    }

    public override AstNode VisitFunctionDeclaration(GASParser.FunctionDeclarationContext context)
    {
        var returnType = context.allTypes()[0].Accept(this) as Type;
        var identifier = new Identifier(context.IDENTIFIER()[0].GetText());

        var types = context.allTypes().Skip(1).ToList();
        var identifiers = context.IDENTIFIER().Skip(1).ToList();

        var parameters = types.Zip(identifiers, (typeNode, identifierNode) =>
        {
            var type = typeNode.Accept(this) as Type;
            var identifier = new Identifier(identifierNode.GetText()) {LineNum = context.Start.Line};
            return new Parameter(type, identifier);
        }).ToList();

        var statements = context.statement().Select(stmt => stmt.Accept(this)).ToList();
        var body = ToCompound(statements);
        return new FunctionDeclaration(identifier, parameters, body, returnType) {LineNum = context.Start.Line};
    }

    public override Expression VisitRelationExpression(GASParser.RelationExpressionContext context)
    {
        var binaryExpressions = context.binaryExpression().Select(mu => mu.Accept(this) as Expression).ToList();
        var left = binaryExpressions[0] as Expression;
        var operatorIndex = 1;
        for (int i = 1; i < binaryExpressions.Count; i++)
        {
            left = new BinaryOp(left, context.GetChild(operatorIndex).GetText(), binaryExpressions[i]) {LineNum = context.Start.Line};
            operatorIndex += 2;
        }
        
        return left;
    }

    public override Expression VisitBinaryExpression(GASParser.BinaryExpressionContext context)
    {
        var multExpressions = context.multExpression().Select(mu => mu.Accept(this) as Expression).ToList();
        var left = multExpressions[0] as Expression;
        var operatorIndex = 1;
        for (int i = 1; i < multExpressions.Count; i++)
        {
            left = new BinaryOp(left, context.GetChild(operatorIndex).GetText(), multExpressions[i]) {LineNum = context.Start.Line};
            operatorIndex += 2;
        }
        
        return left;
    }

    public override Expression VisitTerm(GASParser.TermContext context)
    {
        if (context.NUM() != null)
        {
            return new Num(context.NUM().GetText()) {LineNum = context.Start.Line};
        }
        else if (context.functionCallTerm() != null)
        {
            return context.functionCallTerm().Accept(this) as FunctionCallTerm;
        }
        else if (context.ALLSTRINGS() != null)
        {
            return new String(context.ALLSTRINGS().GetText()) {LineNum = context.Start.Line};
        }
        else if (context.expression() != null)
        {
            return VisitExpression(context.expression());
        }
        else if (context.GetText() == "true" || context.GetText() == "false")
        {
            return new Boolean(context.GetText()) {LineNum = context.Start.Line};
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
            return new Identifier(context.IDENTIFIER().GetText()) {LineNum = context.Start.Line};
        }
        else
        {
            throw new NotSupportedException($"Term type not supported: {context.GetText()}");
        }
    }

    public override List VisitListTerm(GASParser.ListTermContext context)
    {
        var type = context.type()?.Accept(this) as Type;
        var expressions = context.expression()?.Select(expr => expr.Accept(this) as Expression).ToList();
        return new List(expressions, type) {LineNum = context.Start.Line};
    }

    public override Expression VisitMultExpression(GASParser.MultExpressionContext context)
    {
        var unaryExpressions = context.unaryExpression().Select(mu => mu.Accept(this) as Expression).ToList();
        var left = unaryExpressions[0] as Expression;
        var operatorIndex = 1;
        for (int i = 1; i < unaryExpressions.Count; i++)
        {
            left = new BinaryOp(left, context.GetChild(operatorIndex).GetText(), unaryExpressions[i]) {LineNum = context.Start.Line};
            operatorIndex += 2;
        }
        
        return left;
    }

    public override AstNode VisitUnaryExpression(GASParser.UnaryExpressionContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitUnaryExpression(context);
        }

        var expression = context.term().Accept(this) as Expression;

        return new UnaryOp(context.GetChild(0).GetText(), expression);
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
