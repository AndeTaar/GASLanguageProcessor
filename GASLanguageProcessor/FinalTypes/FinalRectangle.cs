namespace GASLanguageProcessor.FinalTypes;

public class FinalRectangle
{
    public FinalPoint TopLeft { get; set; }
    public FinalPoint BottomRight { get; set; }
    public float Stroke { get; set; }
    public FinalColour FillColour { get; set; }
    public FinalColour StrokeColour { get; set; }

    public float Width => BottomRight.X - TopLeft.X;
    public float Height => BottomRight.Y - TopLeft.Y;

    public FinalRectangle(FinalPoint topLeft, FinalPoint bottomRight, float stroke, FinalColour fillColour, FinalColour strokeColour)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
        Stroke = stroke;
        FillColour = fillColour;
        StrokeColour = strokeColour;
    }


}