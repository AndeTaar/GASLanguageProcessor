using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms.Identifiers;

public class Identifier : Term
{
    public bool Local = false;

    public Identifier(string name, bool local)
    {
        Name = name;
        Local = local;
    }

    public Identifier(string name, string attribute, bool local)
    {
        Name = name;
        Attribute = attribute;
        Local = local;
    }

    public string Name { get; protected set; }

    public string? Attribute { get; set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitIdentifier(this, envT);
    }
}
