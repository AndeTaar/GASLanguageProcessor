namespace GASLanguageProcessor.FinalTypes;

public class FinalPoint
{
    public FinalNum X { get; set; }
    public FinalNum Y { get; set; }

    public FinalPoint(float x, float y)
    {
        X = new FinalNum(x);
        Y = new FinalNum(y);
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}
