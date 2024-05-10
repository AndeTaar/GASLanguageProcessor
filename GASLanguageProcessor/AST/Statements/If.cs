using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class If : Statement
{
    public Expression Condition { get; protected set; }
    public Statement Statements { get; protected set; }
    public Statement? Else { get; protected set; }

    public If(Expression condition, Statement statements, Statement @else)
    {
        Condition = condition;
        Statements = statements;
        Else = @else;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitIfStatement(this, scope);
    }
}
