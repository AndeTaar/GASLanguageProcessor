using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Boolean : Term
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
