namespace GASLanguageProcessor.FinalTypes;

public class FinalList : FinalType
{
    public FinalList(List<object> values)
    {
        Values = values;
    }

    public List<object> Values { get; set; }

    public override string ToString()
    {
        var s = "";
        for (var i = 0; i < Values.Count; i++) s += Values[i] + " ";
        return s;
    }
}