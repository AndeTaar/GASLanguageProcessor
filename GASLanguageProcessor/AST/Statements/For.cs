using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class For : Statement
{
    public Statement Initializer { get; protected set; }
    public Expression Condition { get; protected set; }
    public Expression Increment { get; protected set; }
    public Statement Body { get; protected set; }

    public For(Statement initializer, Expression condition, Expression increment, Statement body)
    {
        Initializer = initializer;
        Condition = condition;
        Increment = increment;
        Body = body;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitFor(this, scope);
    }
}
