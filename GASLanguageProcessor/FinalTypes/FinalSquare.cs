namespace GASLanguageProcessor.FinalTypes;

public class FinalSquare
{
    public FinalPoint TopLeft { get; set; }
    public float Length { get; set; }
    public float Stroke { get; set; }
    public FinalColour FillColour { get; set; }
    public FinalColour StrokeColour { get; set; }


    public FinalSquare(FinalPoint topLeft, float length, float stroke, FinalColour fillColour, FinalColour strokeColour)
    {
        TopLeft = topLeft;
        Length = length;
        Stroke = stroke;
        FillColour = fillColour;
        StrokeColour = strokeColour;
    }
}