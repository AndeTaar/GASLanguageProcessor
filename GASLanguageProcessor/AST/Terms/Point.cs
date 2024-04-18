namespace GASLanguageProcessor.AST.Terms;

public class Point : AstNode
{
    public AstNode X { get; protected set; }
    public AstNode Y { get; protected set; }

    public Point(AstNode x, AstNode y)
    {
        X = x;
        Y = y;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        X.Accept(visitor, indent + "   ");
        Y.Accept(visitor, indent + "   ");
        Console.WriteLine(indent + this.GetType().Name);
        return this;
    }

}