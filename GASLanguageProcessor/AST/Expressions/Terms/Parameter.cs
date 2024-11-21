using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Parameter
{
    public Parameter(Type type, Identifier identifier)
    {
        Type = type;
        Identifier = identifier;
    }

    public Type Type { get; protected set; }
    public Identifier Identifier { get; protected set; }
}