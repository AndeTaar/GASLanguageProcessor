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

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitIdentifier(this, envT);
    }
}
