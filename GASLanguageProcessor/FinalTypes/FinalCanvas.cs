using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalCanvas
{
    public FinalNumber Width { get; set; }
    public FinalNumber Height { get; set; }
    public FinalColor BackgroundColor { get; set; }

    public FinalCanvas(float width, float height, FinalColor backgroundColor)
    {
        Width = new FinalNumber(width);
        Height = new FinalNumber(height);
        BackgroundColor = backgroundColor;
    }
}