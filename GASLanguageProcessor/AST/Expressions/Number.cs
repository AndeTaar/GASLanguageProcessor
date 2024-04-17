namespace GASLanguageProcessor.AST.Expressions;

public class Number : Expression
{
    public float Value { get; protected set; }

    public Number(float value)
    {
        Value = value;
    }
}