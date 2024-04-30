using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Square: Term
{
    public Expression TopLeft { get; protected set; }
    public Expression Length { get; protected set; }

    public Expression Stroke { get; protected set; }

    public Expression Colour { get; protected set; }

    public Expression StrokeColour { get; protected set; }

    public Square(Expression topLeft, Expression length, Expression stroke, Expression colour, Expression strokeColour)
    {
        TopLeft = topLeft;
        Length = length;
        Stroke = stroke;
        Colour = colour;
        StrokeColour = strokeColour;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitSquare(this);
    }
}
