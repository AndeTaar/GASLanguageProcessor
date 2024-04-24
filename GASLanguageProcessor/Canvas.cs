using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor;

public class Canvas : AstNode
{
    public AstNode Width { get; protected set; }

    public AstNode Height { get; protected set; }

    public AstNode BackgroundColour { get; protected set; }

    public Canvas(AstNode width, AstNode height, AstNode backgroundColour)
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
