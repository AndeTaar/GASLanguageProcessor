namespace GASLanguageProcessor.AST.Statements;

public class Print : Statement
{
    public AstNode Expression { get; protected set; }

    public Print(AstNode expression)
    {
        Expression = expression;
    }

    public override AstNode Accept(IAstVisitor visitor)
    {
        var expression = Expression.Accept(visitor);
        Console.WriteLine(this.GetType().Name);
        return this;
    }

}