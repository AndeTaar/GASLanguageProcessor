namespace GASLanguageProcessor.AST.Expressions;

public class BinaryOp : Expression
{
    public AstNode Left { get; protected set; }
    public AstNode Right { get; protected set; }
    public string Op { get; protected set; }

    public BinaryOp(AstNode left, string op, AstNode right)
    {
        Left = left;
        Op = op;
        Right = right;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitBinaryOp(this);
    }
}