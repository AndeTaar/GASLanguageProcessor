using CARLLanguageProcessor.FinalTypes.Colors;

namespace CARLLanguageProcessor.FinalTypes;

public class FinalCircle : FinalType
{
    public FinalCircle(FinalPoint center, float radius, float stroke, FinalColors fillColor, FinalColors strokeColor)
    {
        Center = center;
        Radius = new FinalNum(radius);
        Stroke = new FinalNum(stroke);
        FillColor = fillColor;
        StrokeColor = strokeColor;
    }

    public FinalPoint Center { get; set; }
    public FinalNum Radius { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColors FillColor { get; set; }
    public FinalColors StrokeColor { get; set; }
}
