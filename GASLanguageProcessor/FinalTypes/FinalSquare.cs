namespace GASLanguageProcessor.FinalTypes;

public class FinalSquare
{
    public FinalPoint TopLeft { get; set; }
    public FinalNumber Length { get; set; }
    public FinalNumber Stroke { get; set; }
    public FinalColor FillColor { get; set; }
    public FinalColor StrokeColor { get; set; }


    public FinalSquare(FinalPoint topLeft, float length, float stroke, FinalColor fillColor, FinalColor strokeColor)
    {
        TopLeft = topLeft;
        Length = new FinalNumber(length);
        Stroke = new FinalNumber(stroke);
        FillColor = fillColor;
        StrokeColor = strokeColor;
    }
}