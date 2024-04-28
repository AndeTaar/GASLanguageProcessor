namespace GASLanguageProcessor.FinalTypes;

public class FinalCircle
{  
    public FinalPoint Center { get; set; }
    public float Radius { get; set; }
    public float Stroke { get; set; }
    public FinalColour FillColour { get; set; }
    public FinalColour StrokeColour { get; set; }
    
    
    public FinalCircle(FinalPoint center, float radius, float stroke, FinalColour fillColour, FinalColour strokeColour)
    {
        Center = center;
        Radius = radius;
        Stroke = stroke;
        FillColour = fillColour;
        StrokeColour = strokeColour;
    }
}