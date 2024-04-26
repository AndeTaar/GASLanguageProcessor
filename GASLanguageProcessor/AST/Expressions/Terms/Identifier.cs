using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Identifier : Expression
{
    public string Name { get; protected set; }

    public Identifier(string name)
    {
        Name = name;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitIdentifier(this, scope);
    }
}
