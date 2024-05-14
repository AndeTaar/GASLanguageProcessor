using System.Globalization;

namespace GASLanguageProcessor.FinalTypes;

public class FinalNum
{
    public float Value { get; set; }

    public FinalNum(float value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}