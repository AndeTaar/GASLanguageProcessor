using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalCanvas 
{
    public float Width { get; set; }
    public float Height { get; set; }
    public FinalColor BackgroundColor { get; set; }

    public FinalCanvas(float width, float height, FinalColor backgroundColor)
    {
        Width = width;
        Height = height;
        BackgroundColor = backgroundColor;
    }
}