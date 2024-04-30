using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalCanvas : FinalObject
{
    public float Width { get; set; }
    public float Height { get; set; }
    public FinalColour BackgroundColour { get; set; }

    public FinalCanvas(float width, float height, FinalColour backgroundColour)
    {
        Width = width;
        Height = height;
        BackgroundColour = backgroundColour;
    }
}