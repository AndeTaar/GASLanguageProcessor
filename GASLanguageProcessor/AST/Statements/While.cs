namespace GASLanguageProcessor.AST.Statements;

public class While : Statement
{
    public Expression Condition { get; protected set; }
    public Statement Body { get; protected set; }

    public While(Expression condition, Statement body)
    {
        Condition = condition;
        Body = body;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Condition.Accept(visitor, indent + "   ");
        Body.Accept(visitor, indent + "   ");
        Console.WriteLine(indent + this.GetType().Name);
        return this;
    }
}