namespace GASLanguageProcessor.FinalTypes;

public class FinalColor : FinalType
{
    public FinalColor(Dictionary<string, object> values)
    {
        Red = new FinalNum(values["red"]);
        Green = new FinalNum(values["green"]);
        Blue = new FinalNum(values["blue"]);
        Alpha = new FinalNum(values["alpha"]);
    }

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
    public FinalNum Alpha { get; set; }

    public string ColorToString()
    {
        return $"rgb({Red}, {Green}, {Blue})";
    }
}