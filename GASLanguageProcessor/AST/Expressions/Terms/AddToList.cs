
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class AddToList: Term
{
    public Expression Expression { get; protected set; }

    public AddToList(Expression expression)
    {
        Expression = expression;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitAddToList(this, scope);
    }
}
