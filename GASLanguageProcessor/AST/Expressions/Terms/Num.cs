using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Num : Term
{
    public Num(string value)
    {
        Value = value;
    }

    public string Value { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitNum(this, envT);
    }
}