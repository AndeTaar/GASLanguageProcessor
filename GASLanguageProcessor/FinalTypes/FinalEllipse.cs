namespace GASLanguageProcessor.FinalTypes;

public class FinalEllipse
{
    public FinalPoint Center { get; set; }
    public float RadiusX { get; set; }
    public float RadiusY { get; set; }
    public float Stroke { get; set; }
    public FinalColor Color { get; set; }
    public FinalColor? StrokeColor { get; set; }
    
    public FinalEllipse(FinalPoint center, float radiusX, float radiusY, float stroke, FinalColor color, FinalColor? strokeColor)
    {
        Center = center;
        RadiusX = radiusX;
        RadiusY = radiusY;
        Stroke = stroke;
        Color = color;
        StrokeColor = strokeColor;
    }
}