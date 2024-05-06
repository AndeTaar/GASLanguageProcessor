namespace GASLanguageProcessor.FinalTypes;

public class FinalEllipse
{
    public FinalPoint Center { get; set; }
    public float RadiusX { get; set; }
    public float RadiusY { get; set; }
    public FinalColour Colour { get; set; }
    public FinalColour? BorderColor { get; set; }
    public float? BorderWidth { get; set; }
    
    public FinalEllipse(FinalPoint center, float radiusX, float radiusY, FinalColour colour, FinalColour? borderColor, float? borderWidth)
    {
        Center = center;
        RadiusX = radiusX;
        RadiusY = radiusY;
        Colour = colour;
        BorderColor = borderColor;
        BorderWidth = borderWidth;
    }
}