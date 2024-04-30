using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Line : Term
{
    public Expression Start { get; protected set; }

    public Expression End { get; protected set; }

    public Expression Stroke { get; protected set; }

    public Expression? Colour { get; protected set; }
    public Line(Expression start, Expression end, Expression stroke, Expression? colour)
    {
        Start = start;
        End = end;
        Stroke = stroke;
        Colour = colour;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitLine(this);
    }
}
