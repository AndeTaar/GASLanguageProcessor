namespace GASLanguageProcessor.FinalTypes;

public class FinalList : FinalType
{
    public FinalList(object[] values)
    {
        Values = values;
    }

    public object[] Values { get; set; }

    public override string ToString()
    {
        var s = "";
        for (var i = 0; i < Values.Length; i++) s += Values[i] + " ";
        return s;
    }
}