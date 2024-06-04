using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.RecTerms;

public class RecType: RecTerm
{
    public string Value { get; set; }

    public RecType(string value)
    {
        Value = value;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitRecType(this, envT);
    }
}
