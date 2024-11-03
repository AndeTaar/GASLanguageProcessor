using GASLanguageProcessor.FinalTypes.Colors;

namespace GASLanguageProcessor.FinalTypes;

public class FinalEllipse : FinalType
{
    public FinalEllipse(FinalPoint center, float radiusX, float radiusY, float stroke, FinalColors color,
        FinalColors? strokeColor)
    {
        Center = center;
        RadiusX = new FinalNum(radiusX);
        RadiusY = new FinalNum(radiusY);
        Stroke = new FinalNum(stroke);
        Color = color;
        StrokeColor = strokeColor;
    }

    public FinalPoint Center { get; set; }
    public FinalNum RadiusX { get; set; }
    public FinalNum RadiusY { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColors Color { get; set; }
    public FinalColors? StrokeColor { get; set; }
}