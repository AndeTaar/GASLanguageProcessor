namespace GASLanguageProcessor.FinalTypes;

public class FinalPolygon : FinalType
{
    public FinalPolygon(FinalList points, float stroke, FinalColor color, FinalColor strokeColor)
    {
        Points = points;
        Color = color;
        Stroke = new FinalNum(stroke);
        StrokeColor = strokeColor;
    }

    public FinalList Points { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColor Color { get; set; }
    public FinalColor StrokeColor { get; set; }
}