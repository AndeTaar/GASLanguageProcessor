using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Identifier : Term
{
    public string Name { get; protected set; }

    public Identifier? ChildAttribute { get; set; }

    public Identifier(string name, Identifier? childAttribute = null)
    {
        Name = name;
        ChildAttribute = childAttribute;
    }

    public string ToCompoundIdentifierName()
    {
        if (ChildAttribute == null)
        {
            return Name;
        }
        return $"{Name}.{ChildAttribute.ToCompoundIdentifierName()}";
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitIdentifier(this, scope);
    }
}
