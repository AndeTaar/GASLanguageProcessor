namespace CARLLanguageProcessor.AST.Types;

public class ArrayType: ValueType
{
    public CARLType ElementType { get; set; }

    public ArrayType(CARLType elementType)
    {
        ElementType = elementType;
    }

    public override string ToString()
    {
        return $"Array of {ElementType.ToString()}";
    }

    public override bool Equals(CARLType other)
    {
        return other is ArrayType arrayType && arrayType.ElementType.Equals(ElementType);
    }
}
