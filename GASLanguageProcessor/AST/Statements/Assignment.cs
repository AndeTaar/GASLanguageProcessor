namespace GASLanguageProcessor.AST.Statements;

public class Assignment : Statement
{
    public string Identifier { get; protected set; }
    public AstNode Value { get; protected set; }

    public Assignment(string identifier, AstNode value)
    {
        Identifier = identifier;
        Value = value;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        var expression = Value.Accept(visitor, indent + "   ");
        Console.WriteLine(this.GetType().Name);
        return this;
    }

}