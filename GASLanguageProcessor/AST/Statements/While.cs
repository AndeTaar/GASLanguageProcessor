using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class While : Statement
{
    public While(Expression condition, Statement statements)
    {
        Condition = condition;
        Statements = statements;
    }

    public Expression Condition { get; protected set; }
    public Statement Statements { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitWhile(this, envT);
    }
}