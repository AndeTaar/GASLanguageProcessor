using GASLanguageProcessor.AST.Expressions;

namespace GASLanguageProcessor.AST.Terms;

public class Colour: AstNode
{
    public Number Red { get; protected set; }
    public Number Green { get; protected set; }
    public Number Blue { get; protected set; }
    public Number Alpha { get; protected set; }

    public Colour(Number red, Number green, Number blue, Number alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Console.WriteLine(indent + this.GetType().Name);
        Red.Accept(visitor, indent + "   ");
        Green.Accept(visitor, indent + "   ");
        Blue.Accept(visitor, indent + "   ");
        Alpha.Accept(visitor, indent + "   ");
        return this;
    }
}