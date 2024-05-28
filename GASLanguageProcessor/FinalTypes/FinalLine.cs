namespace GASLanguageProcessor.FinalTypes;

public class FinalLine
{
    public FinalPoint Start { get; set; }
    public FinalPoint End { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColors StrokeColors { get; set; }

    public FinalLine(FinalPoint start, FinalPoint end, float stroke, FinalColors strokeColors)
    {
        Start = start;
        End = end;
        Stroke = new FinalNum(stroke);
        StrokeColors = strokeColors;
    }
}