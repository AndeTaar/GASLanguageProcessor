using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class Return: Statement
{
    public Expression Expression { get; protected set; }

    public Return(Expression expression)
    {
        Expression = expression;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitReturn(this, scope);
    }
}
