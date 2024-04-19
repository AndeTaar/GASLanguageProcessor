using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor;

public class Canvas : AstNode
{
    public int Width { get; protected set; }

    public int Height { get; protected set; }

    public AstNode BackgroundColour { get; protected set; }

    public Canvas(int width, int height, AstNode backgroundColour)
    {
        Width = width;
        Height = height;
        BackgroundColour = backgroundColour;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitCanvas(this);
    }
}