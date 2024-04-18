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

    public override AstNode Accept(IAstVisitor visitor)
    {
        var expression = Expression?.Accept(visitor);
        Console.WriteLine(this.GetType().Name);
        return this;
    }
}