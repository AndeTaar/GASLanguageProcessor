namespace GASLanguageProcessor.FinalTypes;

public class FinalCanvas : FinalType
{
    public FinalCanvas(float width, float height, FinalColor backgroundColor)
    {
        Width = new FinalNum(width);
        Height = new FinalNum(height);
        BackgroundColor = backgroundColor;
    }

    public FinalNum Width { get; set; }
    public FinalNum Height { get; set; }
    public FinalColor BackgroundColor { get; set; }
}