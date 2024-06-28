using CARLLanguageProcessor.FinalTypes.Colors;

namespace CARLLanguageProcessor.FinalTypes;

public class FinalCanvas : FinalType
{
    public FinalCanvas(float width, float height, FinalColors backgroundColor)
    {
        Width = new FinalNum(width);
        Height = new FinalNum(height);
        BackgroundColor = backgroundColor;
    }

    public FinalNum Width { get; set; }
    public FinalNum Height { get; set; }
    public FinalColors BackgroundColor { get; set; }
}
