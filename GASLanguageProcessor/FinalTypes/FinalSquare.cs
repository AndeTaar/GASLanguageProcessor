namespace GASLanguageProcessor.FinalTypes;

public class FinalSquare
{
    public FinalPoint TopLeft { get; set; }
    public float Length { get; set; }
    public float Stroke { get; set; }
    public FinalColor FillColor { get; set; }
    public FinalColor StrokeColor { get; set; }


    public FinalSquare(FinalPoint topLeft, float length, float stroke, FinalColor fillColor, FinalColor strokeColor)
    {
        TopLeft = topLeft;
        Length = length;
        Stroke = stroke;
        FillColor = fillColor;
        StrokeColor = strokeColor;
    }
}