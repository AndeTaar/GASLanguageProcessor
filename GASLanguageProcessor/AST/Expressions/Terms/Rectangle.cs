using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Rectangle: Term
{
    public Expression TopLeft { get; protected set; }

    public Expression BottomRight { get; protected set; }

    public Expression Stroke { get; protected set; }

    public Expression? Color { get; protected set; }

    public Expression? StrokeColor { get; protected set; }

    public Rectangle(Expression topLeft, Expression bottomRight, Expression stroke, Expression? color, Expression? strokeColor)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
        Stroke = stroke;
        Color = color;
        StrokeColor = strokeColor;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitRectangle(this, scope);
    }
}
