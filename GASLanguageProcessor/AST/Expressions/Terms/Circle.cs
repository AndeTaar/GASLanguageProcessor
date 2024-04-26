using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Terms;

public class Circle : Expression
{
    public AstNode Center { get; protected set; }
    public AstNode Radius { get; protected set; }

    public AstNode StrokeWidth { get; protected set; }

    public AstNode Colour { get; protected set; }

    public AstNode StrokeColour { get; protected set; }

    public Circle(AstNode center, AstNode radius, AstNode strokeWidth, AstNode colour, AstNode strokeColour)
    {
        Center = center;
        Radius = radius;
        StrokeWidth = strokeWidth;
        Colour = colour;
        StrokeColour = strokeColour;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitCircle(this, scope);
    }
}
