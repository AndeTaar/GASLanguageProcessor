using GASLanguageProcessor.AST.Expressions;

namespace GASLanguageProcessor.AST.Terms;

public class Circle : AstNode
{
    public AstNode Center { get; protected set; }
    public AstNode Radius { get; protected set; }

    public AstNode StrokeWidth { get; protected set; }

    public AstNode Colour { get; protected set; }

    public AstNode StrokeColour { get; protected set; }

    public Circle(AstNode center, AstNode radius, AstNode strokeWidth, AstNode colour, AstNode strokeColour)
    {
        Center = center;
        Radius = radius;
        StrokeWidth = strokeWidth;
        Colour = colour;
        StrokeColour = strokeColour;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Console.WriteLine(indent + this.GetType().Name);
        Center.Accept(visitor, indent + "   ");
        Radius.Accept(visitor, indent + "   ");
        StrokeWidth?.Accept(visitor, indent + "   ");
        Colour?.Accept(visitor, indent + "   ");
        StrokeColour?.Accept(visitor, indent + "   ");
        return this;
    }

}