using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Colour: Expression
{
    public Expression Red { get; protected set; }
    public Expression Green { get; protected set; }
    public Expression Blue { get; protected set; }
    public Expression Alpha { get; protected set; }

    public Colour(Expression red, Expression green, Expression blue, Expression alpha)
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
