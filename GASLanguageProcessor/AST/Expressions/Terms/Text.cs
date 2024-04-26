using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Text: Expression
{
    public AstNode Value { get; protected set; }
    public AstNode Position { get; protected set; }
    public AstNode Font { get; protected set; }
    public AstNode FontSize { get; protected set; }
    public AstNode? Colour { get; protected set; }

    public Text(AstNode value, AstNode position, AstNode font, AstNode fontSize, AstNode? colour)
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
