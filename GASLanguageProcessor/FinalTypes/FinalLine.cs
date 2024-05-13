namespace GASLanguageProcessor.FinalTypes;

public class FinalLine
{
    public FinalPoint Start { get; set; }
    public FinalPoint End { get; set; }
    public FinalNumber Stroke { get; set; }
    public FinalColor StrokeColor { get; set; }

    public FinalLine(FinalPoint start, FinalPoint end, float stroke, FinalColor strokeColor)
    {
        Start = start;
        End = end;
        Stroke = new FinalNumber(stroke);
        StrokeColor = strokeColor;
    }
}