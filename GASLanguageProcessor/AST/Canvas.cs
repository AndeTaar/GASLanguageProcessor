namespace GASLanguageProcessor.AST;

using GASLanguageProcessor.AST.Terms;

public class Canvas : AstNode
{
    public NumTerm Width { get; protected set; }
    public NumTerm Height { get; protected set; }
    public ColourTerm? Color { get; protected set; }
    
    public Canvas(NumTerm width, NumTerm height, ColourTerm color)
    {
        Width = width;
        Height = height;
        Color = color;
    }
}