namespace GASLanguageProcessor.AST.Terms;

public class Group: AstNode
{
    public List<AstNode> Terms { get; protected set; }

    public Group(List<AstNode> terms)
    {
        Terms = terms;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        foreach (var term in Terms)
        {
            term.Accept(visitor, indent + "   ");
        }
        Console.WriteLine(indent + this.GetType().Name);
        return this;
    }
}