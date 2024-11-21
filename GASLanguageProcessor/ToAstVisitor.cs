using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.AST.Statements;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor;

public class ToAstVisitor : GASBaseVisitor<AstNode>
{
    public override AstNode VisitProgram(GASParser.ProgramContext context)
    {
        var lines = context.children
            .Select(line => line.Accept(this)).ToList();

        return new AST.Expressions.Terms.Program(ToCompound(lines));
    }

    public override AstNode VisitCanvas(GASParser.CanvasContext context)
    {
        var width = context.expression()[0].Accept(this) as Expression;

        var height = context.expression()[1].Accept(this) as Expression;

        var backgroundColor = context.expression()[2].Accept(this) as Expression;

        return new Canvas(width, height, backgroundColor) { LineNum = context.Start.Line };
    }

    public override AstNode VisitIfStatement(GASParser.IfStatementContext context)
    {
        var condition = context.expression().Accept(this) as Expression;

        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        var ifBody = ToCompound(statements);

        var @else = context.elseStatement()?.Accept(this) as Statement;

        return new If(condition, ifBody, @else) { LineNum = context.Start.Line };
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

        var whileBody = ToCompound(statements);

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
            if (increment != null) return new For(declaration, condition, increment, statements);

            return new For(declaration, condition, assignments[0], statements);
        }

        if (increment != null) return new For(assignments[0], condition, increment, statements);

        return new For(assignments[0], condition, assignments[1], statements) { LineNum = context.Start.Line };
    }

    public override AstNode VisitAssignment(GASParser.AssignmentContext context)
    {
        var identifier = context.identifier()?.Accept(this) as Identifier ??
                         context.attributeIdentifier()?.Accept(this) as Identifier;
        if (identifier == null) Console.WriteLine("error in assignment");
        var op = context.GetChild(1).GetText();
        var value = context.expression().Accept(this) as Expression;

        return new Assignment(identifier, value, op) { LineNum = context.Start.Line };
    }

    public override AstNode VisitIncrement(GASParser.IncrementContext context)
    {
        var identifier = context.identifier().Accept(this) as Identifier;
        var op = context.GetChild(1).GetText();

        return new Increment(identifier, op) { LineNum = context.Start.Line };
    }

    public override AstNode VisitIdentifier(GASParser.IdentifierContext context)
    {
        return new Identifier(context.GetText()) { LineNum = context.Start.Line };
    }

    public override AstNode VisitGroupTerm(GASParser.GroupTermContext context)
    {
        var expression = context.expression().Accept(this) as Expression;

        var statements = ToCompound(context.statement()?.Select(c => c.Accept(this)).ToList());

        return new Group(expression, statements);
    }

    public override AstNode VisitDeclaration(GASParser.DeclarationContext context)
    {
        var type = context.type()?.Accept(this) as Type;

        if (type == null) type = context.collectionType().Accept(this) as Type;

        var identifier = context.identifier()?.Accept(this) as Identifier;

        var value = context.expression()?.Accept(this) as Expression;

        return new Declaration(type, identifier, value) { LineNum = context.Start.Line };
    }

    public override AstNode VisitType(GASParser.TypeContext context)
    {
        return new Type(context.GetText()) { LineNum = context.Start.Line };
    }

    public override AstNode VisitCollectionType(GASParser.CollectionTypeContext context)
    {
        return new Type(context.GetText()) { LineNum = context.Start.Line };
    }

    public override AstNode VisitSimpleStatement(GASParser.SimpleStatementContext context)
    {
        return context.GetChild(0)?.Accept(this);
    }

    public override AstNode VisitExpression(GASParser.ExpressionContext context)
    {
        if (context.children.Count == 1) return base.VisitExpression(context);

        var equalExpression = context.equalityExpression().Select(mu => mu.Accept(this) as Expression).ToList();
        var expression = context.expression()?.Accept(this) as Expression;

        return new BinaryOp(equalExpression[0], context.GetChild(1).GetText(), expression ?? equalExpression[1])
            { LineNum = context.Start.Line };
    }

    public override AstNode VisitEqualityExpression(GASParser.EqualityExpressionContext context)
    {
        if (context.children.Count == 1) return base.VisitEqualityExpression(context);

        var left = context.GetChild(0).Accept(this) as Expression;

        var right = context.GetChild(2).Accept(this) as Expression;

        return new BinaryOp(left, context.GetChild(1).GetText(), right);
    }

    public override AstNode VisitRecDefinition(GASParser.RecDefinitionContext context)
    {
        var recordType = context.recordTypeIdentifier().Accept(this) as Type;
        var types = context.allTypes().Select(t => t.Accept(this) as Type).ToList();
        var identifiers = context.identifier().Select(i => i.Accept(this) as Identifier).ToList();

        return new RecordDefinition(recordType, types, identifiers) { LineNum = context.Start.Line };
    }

    public override AstNode VisitRecordTypeIdentifier(GASParser.RecordTypeIdentifierContext context)
    {
        return new Type(context.GetText()) { LineNum = context.Start.Line };
    }

    public override AstNode VisitAllTypes(GASParser.AllTypesContext context)
    {
        return new Type(context.GetText()) { LineNum = context.Start.Line };
    }

    public override AstNode VisitReturnStatement(GASParser.ReturnStatementContext context)
    {
        var expression = context?.expression().Accept(this) as Expression;
        return new Return(expression);
    }

    public override AstNode VisitFunctionCall(GASParser.FunctionCallContext context)
    {
        var identifier = new Identifier(context.identifier().GetText()) { LineNum = context.Start.Line };
        var arguments = context.expression().ToList().Select(expr => expr.Accept(this) as Expression).ToList();
        if (context.Parent is GASParser.ExpressionContext || context.Parent is GASParser.TermContext)
            return new FunctionCallTerm(identifier, arguments) { LineNum = context.Start.Line };
        return new FunctionCallStatement(identifier, arguments) { LineNum = context.Start.Line };
    }

    public override AstNode VisitFunctionDeclaration(GASParser.FunctionDeclarationContext context)
    {
        var returnType = context.allTypes()[0].Accept(this) as Type;
        var identifier = new Identifier(context.identifier()[0].GetText()) { LineNum = context.Start.Line };

        var types = context.allTypes().Skip(1).ToList();
        var identifiers = context.identifier().Skip(1).ToList();

        var parameters = types.Zip(identifiers, (typeNode, identifierNode) =>
        {
            var type = typeNode.Accept(this) as Type;
            var identifier = new Identifier(identifierNode.GetText()) { LineNum = context.Start.Line };
            return new Parameter(type, identifier);
        }).ToList();

        var statements = context.statement().Select(stmt => stmt.Accept(this)).ToList();
        var body = ToCompound(statements);
        return new FunctionDeclaration(identifier, parameters, body, returnType) { LineNum = context.Start.Line };
    }

    public override AstNode VisitRelationExpression(GASParser.RelationExpressionContext context)
    {
        if (context.children.Count == 1) return base.VisitRelationExpression(context);

        var left = context.binaryExpression()[0].Accept(this) as Expression;

        var right = context.binaryExpression()[1].Accept(this) as Expression;

        return new BinaryOp(left, context.GetChild(1).GetText(), right) { LineNum = context.Start.Line };
    }

    public override AstNode VisitBinaryExpression(GASParser.BinaryExpressionContext context)
    {
        if (context.children.Count == 1) return base.VisitBinaryExpression(context);

        var multExpressions = context.multExpression().Select(mu => mu.Accept(this) as Expression).ToList();
        var binaryExpression = context.binaryExpression()?.Accept(this) as Expression;

        return new BinaryOp(multExpressions[0], context.GetChild(1).GetText(), binaryExpression ?? multExpressions[1])
            { LineNum = context.Start.Line };
    }

    public override AstNode VisitAttributeIdentifier(GASParser.AttributeIdentifierContext context)
    {
        var parent = context.identifier()[0].GetText();
        var attribute = context.identifier()[1].GetText();
        return new Identifier(parent, attribute) { LineNum = context.Start.Line };
    }

    public override AstNode VisitTerm(GASParser.TermContext context)
    {
        if (context.NUM() != null)
            return new Num(context.NUM().GetText()) { LineNum = context.Start.Line };
        if (context.functionCall() != null)
            return VisitFunctionCall(context.functionCall());
        if (context.ALLSTRINGS() != null)
            return new String(context.ALLSTRINGS().GetText()) { LineNum = context.Start.Line };
        if (context.expression() != null)
            return VisitExpression(context.expression());
        if (context.GetText() == "true" || context.GetText() == "false")
            return new Boolean(context.GetText()) { LineNum = context.Start.Line };
        if (context.GetText() == "null")
            return new Null();
        if (context.groupTerm() != null)
            return VisitGroupTerm(context.groupTerm());
        if (context.listTerm() != null)
            return VisitListTerm(context.listTerm());
        if (context.recordTerm() != null)
            return context.recordTerm().Accept(this) as Record;
        if (context.attributeIdentifier() != null)
            return context.attributeIdentifier().Accept(this) as Identifier;
        if (context.identifier() != null)
            return context.identifier().Accept(this) as Identifier;
        throw new NotSupportedException($"Term type not supported: {context.GetText()}");
    }

    public override AstNode VisitRecordTerm(GASParser.RecordTermContext context)
    {
        var recordType = context.recordTypeIdentifier().Accept(this) as Type;
        var identifiers = context.identifier().Select(i => i.Accept(this) as Identifier).ToList();
        var expressions = context.expression().Select(e => e.Accept(this) as Expression).ToList();

        return new Record(recordType, identifiers, expressions) { LineNum = context.Start.Line };
    }

    public override AstNode VisitListTerm(GASParser.ListTermContext context)
    {
        var type = context.type()?.Accept(this) as Type ?? context.type().Accept(this) as Type;
        var expressions = context.expression()?.Select(expr => expr.Accept(this) as Expression).ToList();
        return new List(expressions, type) { LineNum = context.Start.Line };
    }

    public override AstNode VisitMultExpression(GASParser.MultExpressionContext context)
    {
        if (context.children.Count == 1) return base.VisitMultExpression(context);

        var notExpressions = context.unaryExpression().Select(ne => ne.Accept(this) as Expression).ToList();
        var multExpression = context.multExpression()?.Accept(this) as Expression;

        return new BinaryOp(notExpressions[0], context.GetChild(1).GetText(), multExpression ?? notExpressions[1])
            { LineNum = context.Start.Line };
    }

    public override AstNode VisitUnaryExpression(GASParser.UnaryExpressionContext context)
    {
        if (context.children.Count == 1) return base.VisitUnaryExpression(context);

        var expression = context.term().Accept(this) as Expression;

        return new UnaryOp(context.GetChild(0).GetText(), expression);
    }

    private static Statement ToCompound(List<AstNode> lines)
    {
        if (lines.Count == 0) return null!;

        if (lines.Count == 1) return lines[0] as Statement;

        if (lines[0] is Compound compound)
            return new Compound(compound.Statement1,
                new Compound(compound.Statement2, ToCompound(lines.Skip(1).ToList())));

        return new Compound(lines[0] as Statement, ToCompound(lines.Skip(1).ToList()));
    }
}