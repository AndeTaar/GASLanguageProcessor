using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Point : Term
{
    public Point(Expression x, Expression y)
    {
        X = x;
        Y = y;
    }

    public Expression X { get; protected set; }
    public Expression Y { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitPoint(this, envT);
    }
}