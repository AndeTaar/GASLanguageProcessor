namespace GASLanguageProcessor.AST.Terms;

public class Text: AstNode
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

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitText(this);
    }
}