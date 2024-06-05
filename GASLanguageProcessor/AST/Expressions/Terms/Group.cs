using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Group : Term
{
    public Group(Expression point, Statement? statements)
    {
        Point = point;
        Statements = statements;
    }

    public Expression Point { get; protected set; }
    public Statement? Statements { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitGroup(this, envT);
    }
}