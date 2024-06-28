using CARLLanguageProcessor.TableType;

namespace CARLLanguageProcessor.AST.Expressions.Terms;

public class Boolean : Term
{
    public Boolean(string value)
    {
        Value = value;
    }

    public string Value { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitBoolean(this, envT);
    }
}
