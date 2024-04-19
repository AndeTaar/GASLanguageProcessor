namespace GASLanguageProcessor.AST.Expressions;

public class Identifier : Expression
{
    public string Name { get; protected set; }

    public Identifier(string name)
    {
        Name = name;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Console.WriteLine(indent + this.GetType().Name + ' ' + this.Name);
        return this;
    }
}
