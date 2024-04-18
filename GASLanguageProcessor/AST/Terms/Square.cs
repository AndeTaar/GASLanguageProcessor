namespace GASLanguageProcessor.AST.Terms;

public class Square: AstNode
{
    public AstNode TopLeft { get; protected set; }
    public AstNode BottomRight { get; protected set; }

    public AstNode StrokeWidth { get; protected set; }

    public AstNode Colour { get; protected set; }

    public AstNode StrokeColour { get; protected set; }

    public Square(AstNode topLeft, AstNode bottomRight, AstNode strokeWidth, AstNode colour, AstNode strokeColour)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
        StrokeWidth = strokeWidth;
        Colour = colour;
        StrokeColour = strokeColour;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Console.WriteLine(indent + this.GetType().Name);
        TopLeft.Accept(visitor, indent + "   ");
        BottomRight.Accept(visitor, indent + "   ");
        StrokeWidth?.Accept(visitor, indent + "   ");
        Colour?.Accept(visitor, indent + "   ");
        StrokeColour?.Accept(visitor, indent + "   ");
        return this;
    }

}