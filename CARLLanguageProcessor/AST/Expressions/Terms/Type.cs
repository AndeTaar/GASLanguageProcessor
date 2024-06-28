using CARLLanguageProcessor.TableType;

namespace CARLLanguageProcessor.AST.Expressions.Terms;

public class Type : Term
{
    public Type(string value)
    {
        Value = value;
    }

    public string Value { get; set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitType(this, envT);
    }
}
