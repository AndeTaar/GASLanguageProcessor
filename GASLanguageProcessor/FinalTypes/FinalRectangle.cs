namespace GASLanguageProcessor.FinalTypes;

public class FinalRectangle
{
    public FinalPoint TopLeft { get; set; }
    public FinalPoint BottomRight { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColor FillColor { get; set; }
    public FinalColor StrokeColor { get; set; }

    public FinalNum Width { get; set; }
    public FinalNum Height { get; set; }

    public FinalRectangle(FinalPoint topLeft, FinalPoint bottomRight, float stroke, FinalColor fillColor, FinalColor strokeColor)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
        Stroke = new FinalNum(stroke);
        FillColor = fillColor;
        StrokeColor = strokeColor;
        Width = new FinalNum(BottomRight.X.Value - TopLeft.X.Value);
        Height = new FinalNum(BottomRight.Y.Value - TopLeft.Y.Value);
    }


}