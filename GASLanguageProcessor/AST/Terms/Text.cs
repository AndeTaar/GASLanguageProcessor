namespace GASLanguageProcessor.AST.Terms;

public class Text: AstNode
{
    public string Value { get; protected set; }
    public AstNode Position { get; protected set; }
    public String Font { get; protected set; }
    public AstNode FontSize { get; protected set; }
    public AstNode? Colour { get; protected set; }

    public Text(string value, AstNode position, string font, AstNode fontSize, AstNode? colour)
    {
        Value = value;
        Position = position;
        Font = font;
        FontSize = fontSize;
        Colour = colour;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Console.WriteLine(indent + this.GetType().Name + ' ' + this.Value);
        Position.Accept(visitor, indent + "   ");
        Console.WriteLine(indent + "   " + this.Font);
        FontSize.Accept(visitor, indent + "   ");
        Colour?.Accept(visitor, indent + "   ");
        return this;
    }
}