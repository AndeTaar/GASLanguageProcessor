using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Point : Term
{
    public Expression X { get; protected set; }
    public Expression Y { get; protected set; }

    public Point(Expression x, Expression y)
    {
        X = x;
        Y = y;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitPoint(this);
    }
}
