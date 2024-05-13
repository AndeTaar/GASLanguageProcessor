namespace GASLanguageProcessor.FinalTypes;

public class FinalRectangle
{
    public FinalPoint TopLeft { get; set; }
    public FinalPoint BottomRight { get; set; }
    public FinalNumber Stroke { get; set; }
    public FinalColor FillColor { get; set; }
    public FinalColor StrokeColor { get; set; }

    public FinalNumber Width { get; set; }
    public FinalNumber Height { get; set; }

    public FinalRectangle(FinalPoint topLeft, FinalPoint bottomRight, float stroke, FinalColor fillColor, FinalColor strokeColor)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
        Stroke = new FinalNumber(stroke);
        FillColor = fillColor;
        StrokeColor = strokeColor;
        Width = new FinalNumber(BottomRight.X.Value - TopLeft.X.Value);
        Height = new FinalNumber(BottomRight.Y.Value - TopLeft.Y.Value);
    }


}