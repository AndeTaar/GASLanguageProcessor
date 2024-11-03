using GASLanguageProcessor.FinalTypes.Colors;

namespace GASLanguageProcessor.FinalTypes;

public class FinalPolygon : FinalType
{
    public FinalPolygon(FinalList points, float stroke, FinalColors color, FinalColors strokeColor)
    {
        Points = points;
        Color = color;
        Stroke = new FinalNum(stroke);
        StrokeColor = strokeColor;
    }

    public FinalList Points { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColors Color { get; set; }
    public FinalColors StrokeColor { get; set; }
}
