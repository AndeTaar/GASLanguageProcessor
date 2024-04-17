namespace GASLanguageProcessor.AST.Statements;

public class Declaration : Statement
{
    public string Identifier { get; protected set; }
    public Expression? Expression { get; protected set; }

    public Declaration(string identifier, Expression? expression)
    {
        Identifier = identifier;
        Expression = expression;
    }
}