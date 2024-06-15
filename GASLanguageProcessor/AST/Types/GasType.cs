namespace GASLanguageProcessor.AST.Types;

public abstract class GasType
{
    public abstract string ToString();

    public abstract bool Equals(GasType other);
}