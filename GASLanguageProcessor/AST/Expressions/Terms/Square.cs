using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Square: Term
{
    public Expression TopLeft { get; protected set; }
    public Expression BottomRight { get; protected set; }

    public Expression Stroke { get; protected set; }

    public Expression Colour { get; protected set; }

    public Expression StrokeColour { get; protected set; }

    public Square(Expression topLeft, Expression bottomRight, Expression stroke, Expression colour, Expression strokeColour)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
        Stroke = stroke;
        Colour = colour;
        StrokeColour = strokeColour;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitSquare(this, scope);
    }
}
