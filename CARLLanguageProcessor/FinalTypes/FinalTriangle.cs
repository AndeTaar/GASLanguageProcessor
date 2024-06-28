using CARLLanguageProcessor.FinalTypes.Colors;

namespace CARLLanguageProcessor.FinalTypes;

public class FinalTriangle : FinalType
{
    public FinalTriangle(FinalList points, float stroke, FinalColors color,
        FinalColors strokeColor)
    {
        Points = points;
        Stroke = new FinalNum(stroke);
        Color = color;
        StrokeColor = strokeColor;
    }

    public FinalList Points { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColors Color { get; set; }
    public FinalColors StrokeColor { get; set; }

    public override string ToString()
    {
        return Points.ToString();
    }
}
