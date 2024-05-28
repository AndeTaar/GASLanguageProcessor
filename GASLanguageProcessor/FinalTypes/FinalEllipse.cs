namespace GASLanguageProcessor.FinalTypes;

public class FinalEllipse
{
    public FinalPoint Center { get; set; }
    public FinalNum RadiusX { get; set; }
    public FinalNum RadiusY { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColors Colors { get; set; }
    public FinalColors? StrokeColor { get; set; }

    public FinalEllipse(FinalPoint center, float radiusX, float radiusY, float stroke, FinalColors colors, FinalColors? strokeColor)
    {
        Center = center;
        RadiusX = new FinalNum(radiusX);
        RadiusY = new FinalNum(radiusY);
        Stroke = new FinalNum(stroke);
        Colors = colors;
        StrokeColor = strokeColor;
    }
}