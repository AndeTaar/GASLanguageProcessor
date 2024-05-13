namespace GASLanguageProcessor.FinalTypes;

public class FinalEllipse
{
    public FinalPoint Center { get; set; }
    public FinalNumber RadiusX { get; set; }
    public FinalNumber RadiusY { get; set; }
    public FinalNumber Stroke { get; set; }
    public FinalColor Color { get; set; }
    public FinalColor? StrokeColor { get; set; }

    public FinalEllipse(FinalPoint center, float radiusX, float radiusY, float stroke, FinalColor color, FinalColor? strokeColor)
    {
        Center = center;
        RadiusX = new FinalNumber(radiusX);
        RadiusY = new FinalNumber(radiusY);
        Stroke = new FinalNumber(stroke);
        Color = color;
        StrokeColor = strokeColor;
    }
}