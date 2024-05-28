namespace GASLanguageProcessor.FinalTypes;

public class FinalRectangle
{
    public FinalPoint TopLeft { get; set; }
    public FinalPoint BottomRight { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColors FillColors { get; set; }
    public FinalColors StrokeColors { get; set; }
    public FinalNum Width { get; set; }
    public FinalNum Height { get; set; }
    public FinalNum CornerRounding { get; set; }

    public FinalRectangle(FinalPoint topLeft, FinalPoint bottomRight, float stroke, FinalColors fillColors, FinalColors strokeColors, float cornerRounding)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
        Stroke = new FinalNum(stroke);
        FillColors = fillColors;
        StrokeColors = strokeColors;
        Width = new FinalNum(BottomRight.X.Value - TopLeft.X.Value);
        Height = new FinalNum(BottomRight.Y.Value - TopLeft.Y.Value);
        CornerRounding = new FinalNum(cornerRounding);
    }
}