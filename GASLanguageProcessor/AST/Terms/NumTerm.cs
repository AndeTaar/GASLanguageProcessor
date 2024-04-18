namespace GASLanguageProcessor.AST.Terms;

public class NumTerm : AstNode
{
    public float Value { get; protected set; }

    public NumTerm(float value)
    {
        Value = value;
    }
}