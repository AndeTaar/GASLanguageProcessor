using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Line : Term
{
    public Expression Start { get; protected set; }

    public Expression End { get; protected set; }

    public Expression Stroke { get; protected set; }

    public Expression? Colour { get; protected set; }

    public Expression? StrokeColour { get; protected set; }

    public Line(Expression start, Expression end, Expression stroke, Expression? colour, Expression? strokeColour)
    {
        Start = start;
        End = end;
        Stroke = stroke;
        Colour = colour;
        StrokeColour = strokeColour;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitLine(this, scope);
    }
}
