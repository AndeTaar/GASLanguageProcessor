namespace GASLanguageProcessor.AST.Terms;

public class ColourTerm : AstNode
{
    public NumTerm Red { get; protected set; }
    public NumTerm Green { get; protected set; }
    public NumTerm Blue { get; protected set; }
    public NumTerm Alpha { get; protected set; }
    public string? name { get; protected set; }

    public ColourTerm(NumTerm red, NumTerm green, NumTerm blue, NumTerm alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }
}