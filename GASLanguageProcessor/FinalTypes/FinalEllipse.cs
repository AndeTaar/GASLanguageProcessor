namespace GASLanguageProcessor.FinalTypes;

public class FinalEllipse
{
    public FinalPoint Center { get; set; }
    public FinalNum RadiusX { get; set; }
    public FinalNum RadiusY { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColor Color { get; set; }
    public FinalColor? StrokeColor { get; set; }

    public FinalEllipse(FinalPoint center, float radiusX, float radiusY, float stroke, FinalColor color, FinalColor? strokeColor)
    {
        Center = center;
        RadiusX = new FinalNum(radiusX);
        RadiusY = new FinalNum(radiusY);
        Stroke = new FinalNum(stroke);
        Color = color;
        StrokeColor = strokeColor;
    }
}