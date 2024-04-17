namespace GASLanguageProcessor.AST;

using GASLanguageProcessor.AST.Terms;

public class Canvas : AstNode
{
    public string Width { get; protected set; }
    public string Height { get; protected set; }
    public ColourTerm? Color { get; protected set; }
    

    public Canvas(string width, string height)
    {
        Width = width;
        Height = height;
    }
    
    public Canvas(string width, string height, ColourTerm color)
    {
        Width = width;
        Height = height;
        Color = color;
    }
}