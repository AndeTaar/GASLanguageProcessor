namespace GASLanguageProcessor.FinalTypes;

public class FinalColor
{
    public FinalNum Red { get; set; }
    public FinalNum Green { get; set; }
    public FinalNum Blue { get; set; }
    public FinalNum Alpha { get; set; }

    public FinalColor(Dictionary<string, object> values)
    {
        Red = new FinalNum(values["red"]);
        Green = new FinalNum(values["green"]);
        Blue = new FinalNum(values["blue"]);
        Alpha = new FinalNum(values["alpha"]);
    }

    public string ColorToString()
    {
        return $"rgb({Red}, {Green}, {Blue})";
    }
}
