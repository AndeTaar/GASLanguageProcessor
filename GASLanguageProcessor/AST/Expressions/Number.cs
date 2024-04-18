namespace GASLanguageProcessor.AST.Expressions;

public class Number : Expression
{
    public string Value { get; protected set; }

    public Number(string value)
    {
        Value = value;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Console.WriteLine(indent + this.GetType().Name);
        return this;
    }
}