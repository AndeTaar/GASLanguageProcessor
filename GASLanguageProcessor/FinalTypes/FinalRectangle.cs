namespace GASLanguageProcessor.FinalTypes;

public class FinalRectangle
{
    public FinalPoint TopLeft { get; set; }
    public FinalPoint BottomRight { get; set; }
    public float Stroke { get; set; }
    public FinalColor FillColor { get; set; }
    public FinalColor StrokeColor { get; set; }

    public float Width => BottomRight.X - TopLeft.X;
    public float Height => BottomRight.Y - TopLeft.Y;

    public FinalRectangle(FinalPoint topLeft, FinalPoint bottomRight, float stroke, FinalColor fillColor, FinalColor strokeColor)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
        Stroke = stroke;
        FillColor = fillColor;
        StrokeColor = strokeColor;
    }


}