namespace GASLanguageProcessor.FinalTypes;

public class FinalColor
{
    public float Red { get; set; }
    public float Green { get; set; }
    public float Blue { get; set; }
    public float Alpha { get; set; }

    public FinalColor(float red, float green, float blue, float alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public string ColorToString()
    {
        return $"rgba({Red}, {Green}, {Blue}, {Alpha})";
    }
}