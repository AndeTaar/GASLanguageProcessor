namespace GASLanguageProcessor.AST.Terms;

public class String: AstNode
{
    public string Value { get; protected set; }

    public String(string value)
    {
        Value = value;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitString(this);
    }

}