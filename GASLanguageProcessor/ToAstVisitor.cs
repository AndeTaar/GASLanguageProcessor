using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using Boolean = GASLanguageProcessor.AST.Expressions.Boolean;
using String = GASLanguageProcessor.AST.Terms.String;
using Type = GASLanguageProcessor.AST.Terms.Type;

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
        var condition = context.expression().Accept(this);

        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        AstNode ifBody = ToCompound(statements);


        AstNode elseBody = context.elseStatement().Accept(this);

        return new If(condition, ifBody, elseBody);
    }

    public override AstNode VisitElseStatement(GASParser.ElseStatementContext context)
    {
        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        AstNode elseBody = ToCompound(statements);

        var condition = context.ifStatement()?.Accept(this) as If;

        return new Else(elseBody, condition);
    }

    public override AstNode VisitWhileStatement(GASParser.WhileStatementContext context)
    {
        var condition = context.expression().Accept(this);
        
        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();
        
        AstNode whileBody = ToCompound(statements);
        
        return new While(condition, whileBody);
    }
    
    public override AstNode VisitForStatement(GASParser.ForStatementContext context)
    {
        // Bad, but we don't know if it's an assignment or declaration.
        var initializer = context.GetChild(2).Accept(this);
        var condition = context.expression().Accept(this);
        // Can't just accept assignment, since the initializer could be an assignment
        var increment = context.GetChild(5).Accept(this);
        
        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();
        
        AstNode forBody = ToCompound(statements);
        
        return new For(initializer, condition, increment, forBody);
    }

    public override AstNode VisitAssignment(GASParser.AssignmentContext context)
    {
        var identifier = new Identifier(context.IDENTIFIER().GetText());

        if (identifier == null)
        {
            throw new Exception("Assignment context is null");
        }

        var value = context.GetChild(2).Accept(this);

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
        var type = context.type().Accept(this);
        var identifier = new Identifier(context.IDENTIFIER().GetText());

        var value = context.expression().Accept(this);

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
        var expression = context?.expression().Accept(this);
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
        var returnType = context.type()[0].Accept(this);
        var identifier = new Identifier(context.IDENTIFIER()[0].GetText());

        var types = context.type().Skip(1).ToList();
        var identifiers = context.IDENTIFIER().Skip(1).ToList();

        var parameters = types.Zip(identifiers, (typeNode, identifierNode) =>
        {
            var type = typeNode.Accept(this);
            var identif = new Identifier(identifierNode.GetText());
            return new Declaration(type, identif, null);
        }).ToList();

        var returnStatement = context.returnStatement()?.Accept(this);
        var statements = ToCompound(context.statement().Select(stmt => stmt.Accept(this)).ToList());
        return new FunctionDeclaration(identifier, parameters, statements,  returnStatement, returnType);
    }

    public override AstNode VisitRelationExpression(GASParser.RelationExpressionContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitRelationExpression(context);
        }

        var left = context.binaryExpression()[0].Accept(this);

        var right = context.binaryExpression()[1].Accept(this);

        return new BinaryOp(left, context.GetChild(1).GetText(), right);
    }

    public override AstNode VisitBinaryExpression(GASParser.BinaryExpressionContext context)
    {
        if (context.children.Count == 1)
        {
            return base.VisitBinaryExpression(context);
        }

        var left = context.multExpression()[0].Accept(this);

        var right = context.multExpression()[1].Accept(this);

        return new BinaryOp(left, context.GetChild(1).GetText(), right);
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
