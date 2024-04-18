namespace GASLanguageProcessor.AST.Terms;

public class Rectangle: AstNode
{
    public AstNode TopLeft { get; protected set; }

    public AstNode BottomRight { get; protected set; }

    public AstNode Stroke { get; protected set; }

    public AstNode? Colour { get; protected set; }

    public AstNode? StrokeColour { get; protected set; }

    public Rectangle(AstNode topLeft, AstNode bottomRight, AstNode stroke, AstNode? colour, AstNode? strokeColour)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
        Stroke = stroke;
        Colour = colour;
        StrokeColour = strokeColour;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Console.WriteLine(indent + this.GetType().Name);
        TopLeft.Accept(visitor, indent + "   ");
        BottomRight.Accept(visitor, indent + "   ");
        Stroke?.Accept(visitor, indent + "   ");
        Colour?.Accept(visitor, indent + "   ");
        StrokeColour?.Accept(visitor, indent + "   ");
        return this;
    }
}