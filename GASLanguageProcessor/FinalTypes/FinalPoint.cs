namespace GASLanguageProcessor.FinalTypes;

public class FinalPoint: FinalObject
{
    public float X { get; set; }
    public float Y { get; set; }

    public FinalPoint(float x, float y)
    {
        X = x;
        Y = y;
    }
}