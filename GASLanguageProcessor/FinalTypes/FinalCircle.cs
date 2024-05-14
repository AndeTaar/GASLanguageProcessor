namespace GASLanguageProcessor.FinalTypes;

public class FinalCircle
{
    public FinalPoint Center { get; set; }
    public FinalNumber Radius { get; set; }
    public FinalNumber Stroke { get; set; }
    public FinalColor FillColor { get; set; }
    public FinalColor StrokeColor { get; set; }


    public FinalCircle(FinalPoint center, float radius, float stroke, FinalColor fillColor, FinalColor strokeColor)
    {
        Center = center;
        Radius = new FinalNumber(radius);
        Stroke = new FinalNumber(stroke);
        FillColor = fillColor;
        StrokeColor = strokeColor;
    }
}