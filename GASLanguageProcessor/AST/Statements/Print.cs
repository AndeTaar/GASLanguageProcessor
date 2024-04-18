namespace GASLanguageProcessor.AST.Statements;

public class Print : Statement
{
    public AstNode Expression { get; protected set; }

    public Print(AstNode expression)
    {
        Expression = expression;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Expression.Accept(visitor, indent + "   ");
        Console.WriteLine(indent + this.GetType().Name);
        return this;
    }
}