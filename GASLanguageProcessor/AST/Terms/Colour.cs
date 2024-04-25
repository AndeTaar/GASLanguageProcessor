using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Terms;

public class Colour: AstNode
{
    public Number Red { get; protected set; }
    public Number Green { get; protected set; }
    public Number Blue { get; protected set; }
    public Number Alpha { get; protected set; }

    public Colour(Number red, Number green, Number blue, Number alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitColour(this, scope);
    }
}