namespace GASLanguageProcessor.FinalTypes;

public class FinalCircle : FinalType
{
    public FinalCircle(FinalPoint center, float radius, float stroke, FinalColor fillColor, FinalColor strokeColor)
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
    public FinalColor FillColor { get; set; }
    public FinalColor StrokeColor { get; set; }
}