namespace CARLLanguageProcessor.AST.Types.VariableType;

public class VariableType : ValueType
{
    public VariableTypes Type { get; set; }

    public VariableType(VariableTypes type)
    {
        Type = type;
    }

    public override string ToString()
    {
        return Type.ToString();
    }

    public override bool Equals(CARLType other)
    {
        return other is VariableType variableType && variableType.Type == Type;
    }
}
