using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Text: Term
{
    public Expression Value { get; protected set; }
    public Expression Position { get; protected set; }
    public Expression Font { get; protected set; }
    public Expression FontSize { get; protected set; }
    public Expression? Colour { get; protected set; }

    public Text(Expression value, Expression position, Expression font, Expression fontSize, Expression? colour)
    {
        Value = value;
        Position = position;
        Font = font;
        FontSize = fontSize;
        Colour = colour;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitText(this, scope);
    }
}
