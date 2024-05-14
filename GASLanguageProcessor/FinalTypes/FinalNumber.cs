using System.Globalization;

namespace GASLanguageProcessor.FinalTypes;

public class FinalNumber
{
    public float Value { get; set; }

    public FinalNumber(float value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}