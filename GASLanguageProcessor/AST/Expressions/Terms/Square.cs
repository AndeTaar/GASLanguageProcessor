using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Square: Term
{
    public Expression TopLeft { get; protected set; }

    public Expression Length { get; protected set; }

    public Expression Stroke { get; protected set; }

    public Expression Color { get; protected set; }

    public Expression StrokeColor { get; protected set; }

    public Expression CornerRounding { get; protected set; }

    public Square(Expression topLeft, Expression length, Expression stroke, Expression color, Expression strokeColor, Expression cornerRounding)
    {
        TopLeft = topLeft;
        Length = length;
        Stroke = stroke;
        Color = color;
        StrokeColor = strokeColor;
        CornerRounding = cornerRounding;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitSquare(this, envT);
    }
}
