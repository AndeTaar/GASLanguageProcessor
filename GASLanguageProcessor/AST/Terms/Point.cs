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

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitPoint(this);
    }
}