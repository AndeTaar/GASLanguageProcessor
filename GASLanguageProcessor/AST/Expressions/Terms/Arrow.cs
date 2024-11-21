using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Arrow : Term
{
    public Arrow(Expression start, Expression end, Expression? stroke, Expression? color)
    {
        Start = start;
        End = end;
        Stroke = stroke;
        Color = color;
    }

    public Expression Start { get; protected set; }
    public Expression End { get; protected set; }
    public Expression? Stroke { get; protected set; }
    public Expression? Color { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitArrow(this, envT);
    }
}