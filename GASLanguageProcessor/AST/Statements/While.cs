using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class While : Statement
{
    public Expression Condition { get; protected set; }
    public Compound Statements { get; protected set; }

    public While(Expression condition, Compound statements)
    {
        Condition = condition;
        Statements = statements;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitWhile(this, scope);
    }
}
