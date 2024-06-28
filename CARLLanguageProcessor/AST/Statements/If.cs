using CARLLanguageProcessor.AST.Expressions;
using CARLLanguageProcessor.TableType;

namespace CARLLanguageProcessor.AST.Statements;

public class If : Statement
{
    public If(Expression condition, Statement statements, Statement @else)
    {
        Condition = condition;
        Statements = statements;
        Else = @else;
    }

    public Expression Condition { get; protected set; }
    public Statement Statements { get; protected set; }
    public Statement? Else { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitIfStatement(this, envT);
    }
}
