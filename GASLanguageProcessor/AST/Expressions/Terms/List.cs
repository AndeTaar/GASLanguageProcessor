
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class List : Term
{

    public List<Expression> Expressions { get; protected set; }

    public List(List<Expression> expressions)
    {
        Expressions = expressions;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitList(this, scope);
    }
}