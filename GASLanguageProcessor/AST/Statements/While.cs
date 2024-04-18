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

    public override AstNode Accept(IAstVisitor visitor)
    {
        var condition = Condition.Accept(visitor);
        var body = Body.Accept(visitor);
        Console.WriteLine(this.GetType().Name);
        return this;
    }

}