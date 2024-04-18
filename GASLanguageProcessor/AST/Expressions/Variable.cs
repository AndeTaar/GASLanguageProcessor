namespace GASLanguageProcessor.AST.Expressions;

public class Variable : Expression
{
    public string Name { get; protected set; }
    public AstNode? Value { get; protected set; }

    public Variable(string name, AstNode? value)
    {
        Name = name;
        Value = value;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Console.WriteLine(indent + this.GetType().Name + ' ' + this.Name);
        return this;
    }
}