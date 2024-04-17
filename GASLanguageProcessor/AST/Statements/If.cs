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
}