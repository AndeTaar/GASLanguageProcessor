using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalCanvas
{
    public FinalNum Width { get; set; }
    public FinalNum Height { get; set; }
    public FinalColor BackgroundColor { get; set; }

    public FinalCanvas(float width, float height, FinalColor backgroundColor)
    {
        Width = new FinalNum(width);
        Height = new FinalNum(height);
        BackgroundColor = backgroundColor;
    }
}