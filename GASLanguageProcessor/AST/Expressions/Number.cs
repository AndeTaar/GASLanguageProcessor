namespace GASLanguageProcessor.AST.Expressions;

public class Number : Expression
{
    public string Value { get; protected set; }

    public Number(string value)
    {
        Value = value;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitNumber(this);
    }
}