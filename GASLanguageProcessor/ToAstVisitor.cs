using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using Boolean = GASLanguageProcessor.AST.Expressions.Boolean;

namespace GASLanguageProcessor;

public class ToAstVisitor : GASBaseVisitor<AstNode> {
    public override AstNode VisitProgram ( GASParser.ProgramContext context )
    {
        var deadKid = context.children[0].Accept(this);
        context.children.RemoveAt(0);
        
        var statements = context.children.Cast<GASParser.StatementContext>()
            .Select ( stmt => (Statement) stmt.Accept(this)).ToList();
        return ToCompound(statements);
    }

    public override AstNode VisitStatement(GASParser.StatementContext context)
    {
        return base.VisitStatement(context);
    }

    public override AstNode VisitCanvas(GASParser.CanvasContext context)
    {
        var width = context.GetChild(2).GetText();
        var height = context.GetChild(4).GetText();
        
        // some might have a lust for more security ;) (is it null? is it negging you?)
        
        if (context.children.Count > 6)
        {
            var color = context.GetChild(6).Accept(this) as ColourTerm;
            return new Canvas(width, height, color);
        }
        
        // Also, here and in colour term, consider if "string" is the best type for these values
        
        return new Canvas(width, height);
    }
    
    public override AstNode VisitColourTerm (GASParser.ColourTermContext context)
    {
        Console.WriteLine(context.GetChild(1).GetText());
        Console.WriteLine(context.GetChild(3).GetText());
        Console.WriteLine(context.GetChild(5).GetText());
        var red = context.GetChild(2).GetText();
        var green = context.GetChild(4).GetText();
        var blue = context.GetChild(6).GetText();
        var alpha = context.GetChild(8).GetText();
        
        return new ColourTerm(red, green, blue, alpha);
    }

    public override AstNode VisitAssignment(GASParser.AssignmentContext context)
    {
        var identifier = context.GetChild(0).GetText();

        if (identifier == null)
        {
            throw new Exception("Assignment context is null");
        }

        var expression = context.GetChild(2).Accept(this) as Expression;

        if (expression == null)
        {
            throw new Exception("Expression is null");
        }
        return new Assignment(identifier, expression);
    }

    public override AstNode VisitDeclaration(GASParser.DeclarationContext context)
    {
        var identifier = context.GetChild(1)?.GetText();

        if (identifier == null)
        {
            throw new Exception("Identifier is null");
        }

        var expression = context.GetChild(3)?.Accept(this) as Expression;

        return new Declaration(identifier, expression);
    }

    public override AstNode VisitPrint(GASParser.PrintContext context)
    {
        var expression = context.GetChild(1).Accept(this);

        if (expression == null)
        {
            throw new Exception("Expression is null");
        }

        return new Print(expression);
    }

    public override AstNode VisitExpression(GASParser.ExpressionContext context)
    {
        return base.VisitExpression(context);
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

    public override AstNode VisitRelationExpression(GASParser.RelationExpressionContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitRelationExpression(context);
        }

        var left = context.GetChild(0).Accept(this);

        var right = context.GetChild(2).Accept(this);

        return new BinaryOp(left, context.GetChild(1).GetText(), right);
    }

    public override AstNode VisitBinaryExpression(GASParser.BinaryExpressionContext context)
    {
        if (context.children.Count == 1)
        {
            return base.VisitBinaryExpression(context);
        }

        var left = context.GetChild(0).Accept(this);

        var right = context.GetChild(2).Accept(this);

        return new BinaryOp(left, context.GetChild(1).GetText(), right);
    }

    public override AstNode VisitMultExpression(GASParser.MultExpressionContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitMultExpression(context);
        }

        return new BinaryOp(context.GetChild(0).Accept(this), context.GetChild(1).GetText(), context.GetChild(2).Accept(this));
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

    /// <summary>
    /// Det her må kunne gøres bedre
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override AstNode VisitTerm(GASParser.TermContext context)
    {
        var isBool = bool.TryParse(context.GetText(), out var boolean);

        if (isBool)
        {
            return new Boolean(boolean);
        }

        var isNumber = float.TryParse(context.GetText(), out var number);

        if (isNumber)
        {
            return new Number(number);
        }

        return new Variable(context.GetText());
    }


    private static Statement ToCompound(List<Statement> statements)
    {
        if(statements.Count == 0)
        {
            return new Compound(null, null);
        }
        
        if(statements.Count == 1)
        {
            return statements[0];
        }

        if (statements[0] is Compound compound)
        {
            return new Compound(compound.Statement1,
                new Compound(compound.Statement2, ToCompound(statements.Skip(1).ToList())));
        }

        return new Compound(statements[0], ToCompound(statements.Skip(1).ToList()));
        }
}