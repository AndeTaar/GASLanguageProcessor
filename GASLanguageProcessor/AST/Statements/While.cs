using GASLanguageProcessor.AST.Expressions;

namespace GASLanguageProcessor.AST.Statements;

public class While : Statement
{
    public Expression Condition { get; protected set; }
    public Statement Statements { get; protected set; }

    public While(Expression condition, Statement statements)
    {
        Condition = condition;
        Statements = statements;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitWhile(this);
    }
}
