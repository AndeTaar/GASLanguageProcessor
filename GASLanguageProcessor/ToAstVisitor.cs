using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using Boolean = GASLanguageProcessor.AST.Expressions.Boolean;

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
        int.TryParse(context.GetChild(2).GetText(), out int width);
        int.TryParse(context.GetChild(4).GetText(), out int height);

        Colour backgroundColour = (context.GetChild(6)?.Accept(this) as Colour)!;

        return new Canvas(width, height, backgroundColour);
    }

    public override AstNode VisitColourTerm(GASParser.ColourTermContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitColourTerm(context);
        }

        var red = context.GetChild(1).Accept(this) as Number;
        var green = context.GetChild(3).Accept(this) as Number;
        var blue = context.GetChild(5).Accept(this) as Number;
        var alpha = context.GetChild(7).Accept(this) as Number;

        return new Colour(red, green, blue, alpha);

    }

    public override AstNode VisitSquareTerm(GASParser.SquareTermContext context)
    {
        var point = context.GetChild(1).Accept(this);

        var side = context.GetChild(5).Accept(this);

        var stroke = context.GetChild(7).Accept(this);

        var colour = context.GetChild(9)?.Accept(this);

        var fillColour = context.GetChild(11)?.Accept(this);

        return new Square(point, side, stroke, colour, fillColour);
    }

    public override AstNode VisitRectangleTerm(GASParser.RectangleTermContext context)
    {
        var topLeft = context.GetChild(1).Accept(this);

        var bottomRight = context.GetChild(5).Accept(this);

        var stroke = context.GetChild(7).Accept(this);

        var colour = context.GetChild(9)?.Accept(this);

        var fillColour = context.GetChild(11)?.Accept(this);

        return new Rectangle(topLeft, bottomRight, stroke, colour, fillColour);
    }

    public override AstNode VisitLineTerm(GASParser.LineTermContext context)
    {
        var start = context.GetChild(1).Accept(this);

        var end = context.GetChild(3).Accept(this);

        var stroke = context.GetChild(5).Accept(this);

        var colour = context.GetChild(7)?.Accept(this);

        var strokeColour = context.GetChild(9)?.Accept(this);

        return new Line(start, end, stroke, colour, strokeColour);
    }

    public override AstNode VisitBoolTerm(GASParser.BoolTermContext context)
    {
        return new Boolean(context.GetChild(0).GetText());
    }

    public override AstNode VisitNumTerm(GASParser.NumTermContext context)
    {
        return new Number(context.GetChild(0).GetText());
    }

    public override AstNode VisitAssignment(GASParser.AssignmentContext context)
    {
        var identifier = context.GetChild(0).GetText();

        if (identifier == null)
        {
            throw new Exception("Assignment context is null");
        }

        var value = context.GetChild(2).Accept(this) as AstNode;

        if (value == null)
        {
            throw new Exception("Expression is null");
        }
        return new Assignment(identifier, value);
    }

    public override AstNode VisitIdentifierTerm(GASParser.IdentifierTermContext context)
    {
        return new Variable(context.GetChild(0).GetText(), null);
    }

    public override AstNode VisitDeclaration(GASParser.DeclarationContext context)
    {
        var identifier = context.GetChild(1)?.GetText();

        if (identifier == null)
        {
            throw new Exception("Identifier is null");
        }

        var value = context.GetChild(3)?.Accept(this) as AstNode;

        return new Declaration(identifier, value);
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

    public override AstNode VisitGroupDeclaration(GASParser.GroupDeclarationContext context)
    {
        var statementNodes = context.children
            .Where(child => child is GASParser.StatementContext)
            .Cast<GASParser.StatementContext>()
            .ToList();

        var statements = statementNodes.Select(statement => statement.Accept(this)).ToList();

        return new Group(statements);
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

    public override AstNode VisitCircleTerm(GASParser.CircleTermContext context)
    {
        var point = context.GetChild(1).Accept(this);

        var radius = context.GetChild(5).Accept(this);

        var stroke = context.GetChild(7).Accept(this);

        var colour = context.GetChild(9)?.Accept(this);

        var fillColour = context.GetChild(11)?.Accept(this);

        return new Circle(point, radius, stroke, colour, fillColour);
    }

    public override AstNode VisitPointTerm(GASParser.PointTermContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitPointTerm(context);
        }

        var x = context.GetChild(1).Accept(this);

        var y = context.GetChild(3).Accept(this);

        return new Point(x, y);
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

    private static AstNode ToCompound(List<AstNode> lines)
    {
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