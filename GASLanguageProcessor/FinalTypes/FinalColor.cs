namespace GASLanguageProcessor.FinalTypes;

public class FinalColor
{
    public FinalNum Red { get; set; }
    public FinalNum Green { get; set; }
    public FinalNum Blue { get; set; }
    public FinalNum Alpha { get; set; }

    public FinalColor(float red, float green, float blue, float alpha)
    {
        Red = new FinalNum(red);
        Green = new FinalNum(green);
        Blue = new FinalNum(blue);
        Alpha = new FinalNum(alpha);
    }

    public string ColorToString()
    {
        return $"rgba({Red}, {Green}, {Blue}, {Alpha})";
    }
}