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
}