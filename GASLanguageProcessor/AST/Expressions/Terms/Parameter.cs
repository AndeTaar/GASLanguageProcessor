namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Parameter
{
    public Type Type { get; protected set; }
    public Identifier Identifier { get; protected set; }

    public Parameter(Type type, Identifier identifier)
    {
        Type = type;
        Identifier = identifier;
    }
}