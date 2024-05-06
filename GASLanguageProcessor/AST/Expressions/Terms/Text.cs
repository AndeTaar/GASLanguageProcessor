using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Text: Term
{
    public Expression Value { get; protected set; }
    public Expression Position { get; protected set; }
    public Expression Font { get; protected set; }
    public Expression FontSize { get; protected set; }
    public Expression Color { get; protected set; }

    public Text(Expression value, Expression position, Expression font, Expression fontSize, Expression color)
    {
        Value = value;
        Position = position;
        Font = font;
        FontSize = fontSize;
        Color = color;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitText(this);
    }
}
