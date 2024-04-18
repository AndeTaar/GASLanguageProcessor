namespace GASLanguageProcessor.AST.Statements;

public class If : Statement
{
    public Expression Condition { get; protected set; }
    public Statement Then { get; protected set; }
    public Statement Else { get; protected set; }

    public If(Expression condition, Statement then, Statement @else)
    {
        Condition = condition;
        Then = then;
        Else = @else;
    }

    public override AstNode Accept(IAstVisitor visitor)
    {
        var condition = Condition.Accept(visitor);
        var then = Then.Accept(visitor);
        var @else = Else.Accept(visitor);
        Console.WriteLine(this.GetType().Name);
        return this;
    }
}