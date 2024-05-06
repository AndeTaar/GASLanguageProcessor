namespace GASLanguageProcessor.FinalTypes;

public class FinalSegLine
{
    public FinalPoint Start { get; set; }
    public FinalPoint End { get; set; }
    public float Stroke { get; set; }
    public FinalColour StrokeColour { get; set; }


    public FinalSegLine(FinalPoint start, FinalPoint end, float stroke, FinalColour strokeColour)
    {
        Start = start;
        End = end;
        Stroke = stroke;
        StrokeColour = strokeColour;
    }
}