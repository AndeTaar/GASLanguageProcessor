using System.Globalization;

namespace GASLanguageProcessor.FinalTypes;

public class FinalNum: FinalType
{
    public float Value { get; set; }

    public FinalNum(object value)
    {
        Value = (float) value;
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}
