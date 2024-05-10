namespace GASLanguageProcessor.FinalTypes;

public class FinalPoint
{
    public float X { get; set; }
    public float Y { get; set; }

    public FinalPoint(float x, float y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}
