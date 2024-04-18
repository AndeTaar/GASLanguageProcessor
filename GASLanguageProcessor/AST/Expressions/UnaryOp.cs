namespace GASLanguageProcessor.AST.Expressions;

public class UnaryOp :  Expression
{
    public AstNode Expression { get; protected set; }
    public string Op { get; protected set; }

    public UnaryOp(string op, AstNode expression)
    {
        Op = op;
        Expression = expression;
    }

    public override AstNode Accept(IAstVisitor visitor)
    {
        var expression = Expression.Accept(visitor);
        Console.WriteLine(this.GetType().Name);
        return this;
    }
}