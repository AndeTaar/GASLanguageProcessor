namespace CARLLanguageProcessor.AST.Types;

public abstract class CARLType
{
    public abstract string ToString();

    public abstract bool Equals(CARLType other);
}
