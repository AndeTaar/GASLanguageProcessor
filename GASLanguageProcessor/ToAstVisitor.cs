using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Declarations;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using Boolean = GASLanguageProcessor.AST.Expressions.Boolean;

namespace GASLanguageProcessor;

public class ToAstVisitor : GASBaseVisitor<AstNode> {
    public override AstNode VisitProgram ( GASParser.ProgramContext context )
    {
        // List might be excessive, since there will always be one canvas only one canvas.
        // However, this might be more readable than having a one-liner adding everything but canvas to a list.
        var canvas = new List<Canvas>();
        var statements = new List<Statement>();
        
        foreach (var kid in context.children)
        {
            switch (kid.GetType().ToString().Split("+")[1])
            {
                case "CanvasContext": canvas.Add(kid.Accept(this) as Canvas);
                    break;
                case "StatementContext": statements.Add(kid.Accept(this) as Statement);
                    break;
                case "DeclarationContext": statements.Add(kid.Accept(this) as Declaration);
                    break;
                default:
                    throw new NotImplementedException("Unknown context type: " + kid.GetType().ToString().Split("+")[1]);
            }
        }
        
        return ToCompound(statements);
    }

    public override AstNode VisitStatement(GASParser.StatementContext context)
    {
        return base.VisitStatement(context);
    }

    public override AstNode VisitCanvas(GASParser.CanvasContext context)
    {
        var widthIsNum = float.TryParse(context.GetChild(2).GetText(), out var width);
        var heightIsNum = float.TryParse(context.GetChild(4).GetText(), out var height);
        var color = context.GetChild(6).Accept(this) as ColourTerm;
        
        if (!widthIsNum || !heightIsNum)
        {
            throw new Exception("Width or height is not a number.");
        }
        
        return new Canvas(new NumTerm(width), new NumTerm(height), color);
    }

    public override AstNode VisitColourTerm (GASParser.ColourTermContext context)
    {
        if (context.children.Count == 1)
        {
            return new Variable(context.GetChild(0).GetText());
        }

        var red = context.GetChild(1).Accept(this) as NumTerm;
        var green = context.GetChild(3).Accept(this) as NumTerm;
        var blue = context.GetChild(5).Accept(this) as NumTerm;
        var alpha = context.GetChild(7).Accept(this) as NumTerm;

        if (red == null || green == null || blue == null || alpha == null)
        {
            throw new Exception("One or more of the colours' NumTerms are null");
        }
        
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

    public override AstNode VisitColourDeclaration(GASParser.ColourDeclarationContext context)
    {
        var identifier = context.GetChild(1).GetText();

        if (identifier == null)
        {
            throw new Exception("Identifier is null");
        }

        var colorTerm = context.GetChild(3).Accept(this) as ColourTerm;
        
        if (colorTerm == null)
        {
            throw new Exception("ColorTerm is null");
        }

        var x = new ColourDeclaration(new Variable(identifier), colorTerm);
        Console.WriteLine($"Name: {x.Identifier.Name}, Color: {x.Color.Red.Value}, {x.Color.Green.Value}, {x.Color.Blue.Value}, {x.Color.Alpha.Value}");
        return x;
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

        // should this not be child 1?
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
        
        return base.VisitTerm(context);
    }
    
    public override AstNode VisitNumTerm(GASParser.NumTermContext context)
        {

            var termIsNum = float.TryParse(context.GetChild(0).GetText(), out var num);
            if (!termIsNum)
            {
                return new Variable(context.GetChild(0).GetText());
            }

            return new NumTerm(num);
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