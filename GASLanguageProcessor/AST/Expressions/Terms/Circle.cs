using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Circle : Term
{
    public Circle(Expression center, Expression radius, Expression stroke, Expression color, Expression strokeColor)
    {
        Center = center;
        Radius = radius;
        Stroke = stroke;
        Color = color;
        StrokeColor = strokeColor;
    }

    public Expression Center { get; protected set; }

    public Expression Radius { get; protected set; }

    public Expression Stroke { get; protected set; }

    public Expression Color { get; protected set; }

    public Expression StrokeColor { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitCircle(this, envT);
    }
}