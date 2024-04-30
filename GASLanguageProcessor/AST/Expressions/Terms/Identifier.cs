using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Identifier : Term
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
