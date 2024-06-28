namespace CARLLanguageProcessor.FinalTypes;

public class FinalPoint : FinalType
{
    public FinalPoint(float x, float y)
    {
        X = new FinalNum(x);
        Y = new FinalNum(y);
    }

    public FinalNum X { get; set; }
    public FinalNum Y { get; set; }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}
