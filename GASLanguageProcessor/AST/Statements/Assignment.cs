namespace GASLanguageProcessor.AST.Statements;

public class Assignment : Statement
{
    public string Identifier { get; protected set; }
    public Expression Expression { get; protected set; }

    public Assignment(string identifier, Expression expression)
    {
        Identifier = identifier;
        Expression = expression;
    }

}