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

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Condition.Accept(visitor, indent + "   ");
        Then.Accept(visitor, indent + "   ");
        Else.Accept(visitor, indent + "   ");
        Console.WriteLine(indent + this.GetType().Name);
        return this;
    }
}