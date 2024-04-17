namespace GASLanguageProcessor.AST.Terms;

public class ColourTerm : AstNode
{
    public string Red { get; protected set; }
    public string Green { get; protected set; }
    public string Blue { get; protected set; }
    public string Alpha { get; protected set; }

    public ColourTerm(string red, string green, string blue, string alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }
}