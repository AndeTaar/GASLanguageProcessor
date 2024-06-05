namespace GASLanguageProcessor.FinalTypes;

public class FinalLine : FinalType
{
    public FinalLine(FinalPoint start, FinalPoint end, float stroke, FinalColor strokeColor)
    {
        Start = start;
        End = end;
        Stroke = new FinalNum(stroke);
        StrokeColor = strokeColor;
    }

    public FinalPoint Start { get; set; }
    public FinalPoint End { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColor StrokeColor { get; set; }
}