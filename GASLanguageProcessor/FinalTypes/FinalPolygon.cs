namespace GASLanguageProcessor.FinalTypes;

public class FinalPolygon
{
    public FinalList Points { get; set; }
    public FinalNumber Stroke { get; set; }
    public FinalColor Color { get; set; }
    public FinalColor StrokeColor { get; set; }

    public FinalPolygon(FinalList points, float stroke, FinalColor color, FinalColor strokeColor)
    {
        Points = points;
        Color = color;
        Stroke = new FinalNumber(stroke);
        StrokeColor = strokeColor;
    }

}
