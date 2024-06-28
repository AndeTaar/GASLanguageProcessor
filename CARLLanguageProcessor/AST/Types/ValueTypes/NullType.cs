namespace CARLLanguageProcessor.AST.Types;

public class NullType: ValueType
{
    public override string ToString()
    {
        return "Null";
    }

    public override bool Equals(CARLType other)
    {
        return other is NullType;
    }
}
