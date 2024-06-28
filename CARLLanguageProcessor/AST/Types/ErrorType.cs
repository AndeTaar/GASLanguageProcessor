namespace CARLLanguageProcessor.AST.Types;

public class ErrorType: CARLType
{
    public override string ToString()
    {
        return "Error";
    }

    public override bool Equals(CARLType other)
    {
        return other is ErrorType;
    }
}
