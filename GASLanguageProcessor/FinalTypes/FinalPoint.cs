namespace GASLanguageProcessor.FinalTypes;

public class FinalPoint
{
    public FinalNumber X { get; set; }
    public FinalNumber Y { get; set; }

    public FinalPoint(float x, float y)
    {
        X = new FinalNumber(x);
        Y = new FinalNumber(y);
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}
