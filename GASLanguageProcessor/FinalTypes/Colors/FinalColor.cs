using GASLanguageProcessor.FinalTypes.Colors;

namespace GASLanguageProcessor.FinalTypes;

public class FinalColor : FinalColors
{
    public FinalColor(float red, float green, float blue, float alpha)
    {
        Red = new FinalNum(red);
        Green = new FinalNum(green);
        Blue = new FinalNum(blue);
        Alpha = new FinalNum(alpha);
    }

    public FinalNum Red { get; set; }
    public FinalNum Green { get; set; }
    public FinalNum Blue { get; set; }

    public override string ColorToString()
    {
        return $"rgb({Red}, {Green}, {Blue})";
    }
}