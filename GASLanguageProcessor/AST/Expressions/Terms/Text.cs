using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Text : Term
{
    public Text(Expression value, Expression position, Expression font, Expression fontSize, Expression fontWeight,
        Expression color)
    {
        Value = value;
        Position = position;
        Font = font;
        FontSize = fontSize;
        FontWeight = fontWeight;
        Color = color;
    }

    public Expression Value { get; protected set; }
    public Expression Position { get; protected set; }
    public Expression Font { get; protected set; }
    public Expression FontSize { get; protected set; }
    public Expression? FontWeight { get; protected set; }
    public Expression Color { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitText(this, envT);
    }
}