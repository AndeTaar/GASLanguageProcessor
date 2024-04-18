namespace GASLanguageProcessor.AST.Terms;

public class Colour: AstNode
{
    public int Red { get; protected set; }
    public int Green { get; protected set; }
    public int Blue { get; protected set; }
    public int Alpha { get; protected set; }

    public Colour(int red, int green, int blue, int alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public override AstNode Accept(IAstVisitor visitor)
    {
        Console.WriteLine(this.GetType().Name);
        return this;
    }
}