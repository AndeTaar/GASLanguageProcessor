namespace GASLanguageProcessor.AST.Expressions;

public class Boolean : Expression
{
    public bool Value { get; protected set; }

    public Boolean(bool value)
    {
        Value = value;
    }
}