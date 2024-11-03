namespace GASLanguageProcessor.AST.Types;

public class ArrayType: ValueType
{
    public GasType ElementType { get; set; }

    public ArrayType(GasType elementType)
    {
        ElementType = elementType;
    }

    public override string ToString()
    {
        return $"Array of {ElementType.ToString()}";
    }

    public override bool Equals(GasType other)
    {
        return other is ArrayType arrayType && arrayType.ElementType.Equals(ElementType);
    }
}
