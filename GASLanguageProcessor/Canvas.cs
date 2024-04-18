using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor;

public class Canvas : AstNode
{
    public int Width { get; protected set; }

    public int Height { get; protected set; }

    public Colour BackgroundColour { get; protected set; }

    public Canvas(int width, int height, Colour backgroundColour)
    {
        Width = width;
        Height = height;
        BackgroundColour = backgroundColour;
    }

    public override AstNode Accept(IAstVisitor visitor)
    {
        Console.WriteLine(this.GetType().Name);
        return this;
    }
}