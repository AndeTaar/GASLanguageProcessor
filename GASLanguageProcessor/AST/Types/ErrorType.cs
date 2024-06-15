namespace GASLanguageProcessor.AST.Types;

public class ErrorType: GasType
{
    public override string ToString()
    {
        return "Error";
    }

    public override bool Equals(GasType other)
    {
        return other is ErrorType;
    }
}