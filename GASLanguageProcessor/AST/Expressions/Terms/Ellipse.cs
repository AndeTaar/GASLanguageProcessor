namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Ellipse: Term
{
    public Expression Center { get; protected set; }
    public Expression RadiusX { get; protected set; }
    public Expression RadiusY { get; protected set; }
    public Expression Color { get; protected set; }
    public Expression BorderColor { get; protected set; }
    public Expression Stroke { get; protected set; }

    public Ellipse(Expression center, Expression radiusX, Expression radiusY, Expression stroke, Expression color, Expression? borderColor)
    {
        Center = center;
        RadiusX = radiusX;
        RadiusY = radiusY;
        Stroke = stroke;
        Color = color;
        BorderColor = borderColor;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitEllipse(this);
    }
}