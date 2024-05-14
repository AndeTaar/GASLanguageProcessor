namespace GASLanguageProcessor.FinalTypes;

public class FinalColor
{
    public FinalNumber Red { get; set; }
    public FinalNumber Green { get; set; }
    public FinalNumber Blue { get; set; }
    public FinalNumber Alpha { get; set; }

    public FinalColor(float red, float green, float blue, float alpha)
    {
        Red = new FinalNumber(red);
        Green = new FinalNumber(green);
        Blue = new FinalNumber(blue);
        Alpha = new FinalNumber(alpha);
    }

    public string ColorToString()
    {
        return $"rgba({Red}, {Green}, {Blue}, {Alpha})";
    }
}