namespace GASLanguageProcessor.FinalTypes;

public class FinalColour
{
    public float Red { get; set; }
    public float Green { get; set; }
    public float Blue { get; set; }
    public float Alpha { get; set; }
    
    public FinalColour(float red, float green, float blue, float alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }
    
    public string ColourToString()
    {
        return $"rgb({Red}, {Green}, {Blue})";
    }
}