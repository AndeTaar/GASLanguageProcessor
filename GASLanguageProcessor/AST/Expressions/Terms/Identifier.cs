using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Identifier : Term
{
    public string Name { get; protected set; }

    public string? AttributeName { get; set; }

    public Identifier(string name, string? attributeName = null)
    {
        Name = name;
        AttributeName = attributeName;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitIdentifier(this, scope);
    }
}
