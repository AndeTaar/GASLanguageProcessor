namespace GASLanguageProcessor.AST.Statements;

public class Declaration : Statement
{
    public string Identifier { get; protected set; }
    public AstNode? Value { get; protected set; }

    public Declaration(string identifier, AstNode? value)
    {
        Identifier = identifier;
        Value = value;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        var expression = Value?.Accept(visitor, indent + "   ");
        Console.WriteLine(indent + this.GetType().Name + ' ' + this.Identifier);
        return this;
    }
}