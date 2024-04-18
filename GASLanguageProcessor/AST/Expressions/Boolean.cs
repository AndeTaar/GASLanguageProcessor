namespace GASLanguageProcessor.AST.Expressions;

public class Boolean : Expression
{
    public string Value { get; protected set; }

    public Boolean(string value)
    {
        Value = value;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Console.WriteLine(indent + this.GetType().Name);
        return this;
    }
}