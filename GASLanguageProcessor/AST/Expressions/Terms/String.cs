using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class String : Term
{
    public String(string value)
    {
        Value = value;
    }

    public string Value { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitString(this, envT);
    }
}