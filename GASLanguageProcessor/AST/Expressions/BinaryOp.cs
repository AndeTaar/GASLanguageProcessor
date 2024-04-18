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

    public override AstNode Accept(IAstVisitor visitor)
    {
        var left = Left.Accept(visitor);

        var right = Right.Accept(visitor);

        Console.WriteLine(this.GetType().Name);
        return this;
    }
}