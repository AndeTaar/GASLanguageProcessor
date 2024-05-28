using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Polygon: Term
{
    public Expression Points { get; protected set; }

    public Expression Stroke { get; protected set; }

    public Expression Color { get; protected set; }

    public Expression StrokeColor { get; protected set; }

    public Polygon(Expression points, Expression stroke, Expression color, Expression strokeColor)
    {
        Points = points;
        Stroke = stroke;
        Color = color;
        StrokeColor = strokeColor;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitPolygon(this, envT);
    }
}
