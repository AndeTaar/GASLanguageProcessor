using CARLLanguageProcessor.FinalTypes.Colors;

namespace CARLLanguageProcessor.FinalTypes;

public class FinalSquare : FinalType
{
    public FinalSquare(FinalPoint topLeft, float length, float stroke, FinalColors fillColor, FinalColors strokeColor,
        float cornerRounding)
    {
        TopLeft = topLeft;
        Length = new FinalNum(length);
        Stroke = new FinalNum(stroke);
        FillColor = fillColor;
        StrokeColor = strokeColor;
        CornerRounding = new FinalNum(cornerRounding);
    }

    public FinalPoint TopLeft { get; set; }
    public FinalNum Length { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColors FillColor { get; set; }
    public FinalColors StrokeColor { get; set; }
    public FinalNum CornerRounding { get; set; }
}
