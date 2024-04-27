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
        AstNode width = context.expression()[0].Accept(this);

        AstNode height = context.expression()[1].Accept(this);

        AstNode backgroundColour = context.expression()[2]?.Accept(this)!;

        if(backgroundColour == null)
        {
            throw new Exception("Background colour is null");
        }

        return new Canvas(width, height, backgroundColour);
    }

    public override AstNode VisitIfStatement(GASParser.IfStatementContext context)
    {
        Expression condition = context.expression().Accept(this) as Expression;

        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        Compound ifBody = ToCompound(statements) as Compound;

        var @else = context.elseStatement().Accept(this);

        Compound? elseBody = @else as Compound;

        If? @if = @else as If;

        if(@else != null && (elseBody == null && @if == null))
        {
            throw new Exception("Else is not a compound or if statement");
        }

        return new If(condition, ifBody, @if != null ? @if : elseBody);
    }

    public override AstNode VisitElseStatement(GASParser.ElseStatementContext context)
    {
        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        AstNode elseBody = ToCompound(statements);

        var elseIf = context.ifStatement()?.Accept(this) as If;

        return elseIf ?? elseBody;
    }

    public override AstNode VisitWhileStatement(GASParser.WhileStatementContext context)
    {
        Expression condition = context.expression().Accept(this) as Expression;

        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        Compound whileBody = ToCompound(statements) as Compound;

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

        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        Compound forBody = ToCompound(statements) as Compound;

        if (declaration != null)
        {
            return new For(declaration, condition, assignment, forBody);
        }

        return new For(assignment, condition, assignment2, forBody);
    }

    public override AstNode VisitAssignment(GASParser.AssignmentContext context)
    {
        Identifier identifier = new Identifier(context.IDENTIFIER().GetText());

        if (identifier == null)
        {
            throw new Exception("Assignment context is null");
        }

        Expression value = context.expression().Accept(this) as Expression;

        if (value == null)
        {
            throw new Exception("Expression is null");
        }
        return new Assignment(identifier, value);
    }

    public override AstNode VisitGroupDeclaration(GASParser.GroupDeclarationContext context)
    {
        var identifier = new Identifier(context.IDENTIFIER().GetText());
        AstNode point = context.expression().Accept(this);

        var terms = context.statement()
            .Select(c => c.Accept(this))
            .ToList();

        return new Group(identifier, point, terms);
    }

    public override AstNode VisitDeclaration(GASParser.DeclarationContext context)
    {
        var type = context.type().Accept(this) as Type;
        Identifier identifier = new Identifier(context.IDENTIFIER().GetText());

        Expression value = context.expression().Accept(this) as Expression;

        return new Declaration(type, identifier, value);
    }

    public override AstNode VisitType(GASParser.TypeContext context)
    {
        return new Type(context.GetText());
    }

    public override AstNode VisitExpression(GASParser.ExpressionContext context)
    {
        if (context.children.Count == 1)
        {
            return base.VisitExpression(context);
        }

        var left = context.equalityExpression()[0].Accept(this);
        var right = context.equalityExpression()[0].Accept(this);

        return new BinaryOp(left, context.GetChild(1).GetText(), right);
    }

    public override AstNode VisitEqualityExpression(GASParser.EqualityExpressionContext context)
    {
        if (context.children.Count == 1)
        {
            return base.VisitEqualityExpression(context);
        }

        var left = context.GetChild(0).Accept(this);

        var right = context.GetChild(2).Accept(this);

        return new BinaryOp(left, context.GetChild(1).GetText(), right);
    }

    public override AstNode VisitReturnStatement(GASParser.ReturnStatementContext context)
    {
        Expression expression = context?.expression().Accept(this) as Expression;
        return new Return(expression);
    }

    public override AstNode VisitFunctionCall(GASParser.FunctionCallContext context)
    {
        var identifier = new Identifier(context.IDENTIFIER().GetText());
        var parameters = context.expression().ToList().Select(expr => expr.Accept(this)).ToList();
        return new FunctionCall(identifier, parameters);
    }

    public override AstNode VisitFunctionDeclaration(GASParser.FunctionDeclarationContext context)
    {
        Type returnType = context.type()[0].Accept(this) as Type;
        var identifier = new Identifier(context.IDENTIFIER()[0].GetText());

        var types = context.type().Skip(1).ToList();
        var identifiers = context.IDENTIFIER().Skip(1).ToList();

        var parameters = types.Zip(identifiers, (typeNode, identifierNode) =>
        {
            var type = typeNode.Accept(this) as Type;
            var identif = new Identifier(identifierNode.GetText());
            return new Declaration(type, identif, null);
        }).ToList();

        Compound statements = ToCompound(context.statement().Select(stmt => stmt.Accept(this)).ToList()) as Compound;
        Compound returnStatements = ToCompound(context.returnStatement().Select(stmt => stmt.Accept(this)).ToList()) as Compound;
        return new FunctionDeclaration(identifier, parameters, statements,  returnStatements, returnType);
    }

    public override AstNode VisitRelationExpression(GASParser.RelationExpressionContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitRelationExpression(context);
        }

        var left = context.binaryExpression()[0].Accept(this);

        var right = context.binaryExpression()[1].Accept(this);

        return new BinaryOp(left, context.GetChild(1).GetText(), right) {LineNumber = context.Start.Line};
    }

    public override AstNode VisitBinaryExpression(GASParser.BinaryExpressionContext context)
    {
        if (context.children.Count == 1)
        {
            return base.VisitBinaryExpression(context);
        }

        var left = context.multExpression()[0].Accept(this);

        var right = context.multExpression()[1].Accept(this);

        return new BinaryOp(left, context.GetChild(1).GetText(), right) {LineNumber = context.Start.Line};
    }

    public override AstNode VisitTerm(GASParser.TermContext context)
    {
        if (context.NUM() != null)
        {
            return new Number(context.NUM().GetText());
        }
        else if (context.IDENTIFIER() != null)
        {
            return new Identifier(context.IDENTIFIER().GetText());
        }
        else if (context.functionCall() != null)
        {
            return VisitFunctionCall(context.functionCall());
        }
        else if (context.ALLSTRINGS() != null)
        {
            return new String(context.ALLSTRINGS().GetText());
        }
        else if (context.expression() != null)
        {
            return VisitExpression(context.expression());
        }
        else if (context.GetText() == "true" || context.GetText() == "false")
        {
            return new Boolean(context.GetText());
        }
        else if (context.GetText() == "null")
        {
            return new Null();
        }
        else
        {
            throw new NotSupportedException($"Term type not supported: {context.GetText()}");
        }
    }

    public override AstNode VisitMultExpression(GASParser.MultExpressionContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitMultExpression(context);
        }

        var left = context.notExpression()[0].Accept(this);

        var right = context.notExpression()[1].Accept(this);

        return new BinaryOp(left, context.GetChild(1).GetText(), right);
    }

    public override AstNode VisitNotExpression(GASParser.NotExpressionContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitNotExpression(context);
        }

        var expression = context.GetChild(0).Accept(this);

        return new UnaryOp(context.GetChild(0).GetText(), expression);
    }

    private static AstNode ToCompound(List<AstNode> lines)
    {
        if (lines.Count == 0)
        {
            return null!;
        }

        if(lines.Count == 1)
        {
            return lines[0];
        }

        if (lines[0] is Compound compound)
        {
            return new Compound(compound.Statement1,
                new Compound(compound.Statement2, ToCompound(lines.Skip(1).ToList())));
        }

        return new Compound(lines[0], ToCompound(lines.Skip(1).ToList()));
        }
}
