namespace GASLanguageProcessor.FinalTypes;

public class FinalSquare
{
    public FinalPoint TopLeft { get; set; }
    public FinalNum Length { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColors FillColors { get; set; }
    public FinalColors StrokeColors { get; set; }
    public FinalNum CornerRounding { get; set; }

    public FinalSquare(FinalPoint topLeft, float length, float stroke, FinalColors fillColors, FinalColors strokeColors, float cornerRounding)
    {
        TopLeft = topLeft;
        Length = new FinalNum(length);
        Stroke = new FinalNum(stroke);
        FillColors = fillColors;
        StrokeColors = strokeColors;
        CornerRounding = new FinalNum(cornerRounding);
    }
}