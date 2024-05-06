namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Ellipse: Term
{
    public Expression Center { get; protected set; }
    public Expression RadiusX { get; protected set; }
    public Expression RadiusY { get; protected set; }
    public Expression Colour { get; protected set; }
    public Expression? BorderColor { get; protected set; }
    public Expression? BorderWidth { get; protected set; }
    
    public Ellipse(Expression center, Expression radiusX, Expression radiusY, Expression colour, Expression? borderColor, Expression? borderWidth)
    {
        Center = center;
        RadiusX = radiusX;
        RadiusY = radiusY;
        Colour = colour;
        BorderColor = borderColor;
        BorderWidth = borderWidth;
    }
    
    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitEllipse(this);
    }
}