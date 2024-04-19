namespace GASLanguageProcessor.AST.Expressions;

public class AString : AstNode
{
    public string Value { get; protected set; }
    
    
    public AString(string value)
    {
        Value = value;
    }
    
    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Console.WriteLine(indent + this.GetType().Name + ' ' + this.Value);
        return this;
    }
}