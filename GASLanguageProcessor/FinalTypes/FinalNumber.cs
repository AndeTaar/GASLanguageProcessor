using System.Globalization;

namespace GASLanguageProcessor.FinalTypes;

public class FinalNum : FinalType
{
    public FinalNum(object value)
    {
        Value = (float)value;
    }

    public float Value { get; set; }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}