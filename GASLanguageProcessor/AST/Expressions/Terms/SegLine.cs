using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class SegLine : Term
{
    public Expression Start { get; protected set; }

    public Expression End { get; protected set; }
    
    public Expression Stroke { get; protected set; }

    public Expression? Color { get; protected set; }
    public SegLine(Expression start, Expression end, Expression stroke, Expression? color)
    {
        Start = start;
        End = end;
        Stroke = stroke;
        Color = color;
    }
    
    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitSegLine(this, envT);
    }
}
