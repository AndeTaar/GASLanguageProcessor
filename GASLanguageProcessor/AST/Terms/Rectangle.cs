namespace GASLanguageProcessor.AST.Terms;

public class Rectangle: AstNode
{
    public AstNode TopLeft { get; protected set; }
    public AstNode BottomRight { get; protected set; }

    public Rectangle(AstNode topLeft, AstNode bottomRight)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        TopLeft.Accept(visitor, indent + "   ");
        BottomRight.Accept(visitor, indent + "   ");
        Console.WriteLine(indent + this.GetType().Name);
        return this;
    }
}