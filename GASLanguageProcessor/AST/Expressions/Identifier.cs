namespace GASLanguageProcessor.AST.Expressions;

public class Identifier : Expression
{
    public string Name { get; protected set; }

    public Identifier(string name)
    {
        Name = name;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitIdentifier(this);
    }
}
