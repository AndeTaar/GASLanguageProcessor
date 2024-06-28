namespace CARLLanguageProcessor.FinalTypes.Colors;

public abstract class FinalColors: FinalType
{
    public abstract string ColorToString();

    public FinalNum Alpha { get; set; }
}
