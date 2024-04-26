using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Square: Expression
{
    public AstNode TopLeft { get; protected set; }
    public AstNode BottomRight { get; protected set; }

    public AstNode StrokeWidth { get; protected set; }

    public AstNode Colour { get; protected set; }

    public AstNode StrokeColour { get; protected set; }

    public Square(AstNode topLeft, AstNode bottomRight, AstNode strokeWidth, AstNode colour, AstNode strokeColour)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
        StrokeWidth = strokeWidth;
        Colour = colour;
        StrokeColour = strokeColour;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitSquare(this, scope);
    }
}
