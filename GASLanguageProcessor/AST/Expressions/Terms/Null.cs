using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Null: Term
{
    public Null()
    {
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitNull(this);
    }
}
