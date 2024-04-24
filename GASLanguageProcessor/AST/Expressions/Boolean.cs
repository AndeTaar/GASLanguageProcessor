namespace GASLanguageProcessor.AST.Expressions;

public class Boolean : Expression
{
    public string Value { get; protected set; }

    public Boolean(string value)
    {
        Value = value;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitBoolean(this);
    }
}