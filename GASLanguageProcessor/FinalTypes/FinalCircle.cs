namespace GASLanguageProcessor.FinalTypes;

public class FinalCircle
{
    public FinalPoint Center { get; set; }
    public FinalNum Radius { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColors FillColors { get; set; }
    public FinalColors StrokeColors { get; set; }


    public FinalCircle(FinalPoint center, float radius, float stroke, FinalColors fillColors, FinalColors strokeColors)
    {
        Center = center;
        Radius = new FinalNum(radius);
        Stroke = new FinalNum(stroke);
        FillColors = fillColors;
        StrokeColors = strokeColors;
    }
}