namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Ellipse: Term
{
    public Expression Center { get; protected set; }
    public Expression RadiusX { get; protected set; }
    public Expression RadiusY { get; protected set; }
    public Expression Color { get; protected set; }
    public Expression? BorderColor { get; protected set; }
    public Expression? BorderWidth { get; protected set; }
    
    public Ellipse(Expression center, Expression radiusX, Expression radiusY, Expression color, Expression? borderColor, Expression? borderWidth)
    {
        Center = center;
        RadiusX = radiusX;
        RadiusY = radiusY;
        Color = color;
        BorderColor = borderColor;
        BorderWidth = borderWidth;
    }
    
    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitEllipse(this);
    }
}