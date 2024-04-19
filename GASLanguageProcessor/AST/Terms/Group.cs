namespace GASLanguageProcessor.AST.Terms;

public class Group: AstNode
{
    public List<AstNode> Terms { get; protected set; }

    public Group(List<AstNode> terms)
    {
        Terms = terms;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitGroup(this);
    }
}