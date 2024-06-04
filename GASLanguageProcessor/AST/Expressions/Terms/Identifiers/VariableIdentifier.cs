using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms.Identifiers;

public class VariableIdentifier: Identifier
{
    public VariableIdentifier(string name, string? attribute = null)
    {
        Name = name;
        Attribute = attribute;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitVariableIdentifier(this, envT);
    }
}
