namespace GASLanguageProcessor.FinalTypes;

public class FinalEllipse
{
    public FinalPoint Center { get; set; }
    public float RadiusX { get; set; }
    public float RadiusY { get; set; }
    public FinalColor Color { get; set; }
    public FinalColor? BorderColor { get; set; }
    public float? BorderWidth { get; set; }
    
    public FinalEllipse(FinalPoint center, float radiusX, float radiusY, FinalColor color, FinalColor? borderColor, float? borderWidth)
    {
        Center = center;
        RadiusX = radiusX;
        RadiusY = radiusY;
        Color = color;
        BorderColor = borderColor;
        BorderWidth = borderWidth;
    }
}