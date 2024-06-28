using CARLLanguageProcessor.TableType;

namespace CARLLanguageProcessor.AST.Expressions.Terms.Identifiers;

public class Identifier : Term
{
    public Identifier(string name)
    {
        Name = name;
    }

    public Identifier(string name, string attribute)
    {
        Name = name;
        Attribute = attribute;
    }

    public string Name { get; protected set; }

    public string? Attribute { get; set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitIdentifier(this, envT);
    }
}
