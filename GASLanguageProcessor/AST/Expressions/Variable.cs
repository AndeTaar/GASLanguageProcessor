namespace GASLanguageProcessor.AST.Expressions;

public class Variable : Expression
{
    public string Name { get; protected set; }
    public AstNode? Value { get; protected set; }

    public Variable(string name, AstNode? value)
    {
        Name = name;
    }

    public override AstNode Accept(IAstVisitor visitor)
    {
        Console.WriteLine(this.GetType().Name);
        return this;
    }
}