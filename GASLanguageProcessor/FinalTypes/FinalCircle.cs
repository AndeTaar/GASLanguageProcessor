namespace GASLanguageProcessor.FinalTypes;

public class FinalCircle
{
    public FinalPoint Center { get; set; }
    public float Radius { get; set; }
    public float Stroke { get; set; }
    public FinalColor FillColor { get; set; }
    public FinalColor StrokeColor { get; set; }


    public FinalCircle(FinalPoint center, float radius, float stroke, FinalColor fillColor, FinalColor strokeColor)
    {
        Center = center;
        Radius = radius;
        Stroke = stroke;
        FillColor = fillColor;
        StrokeColor = strokeColor;
    }
}