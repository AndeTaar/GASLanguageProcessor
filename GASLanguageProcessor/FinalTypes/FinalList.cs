using System.Collections;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalList
{
    public List<object> Values { get; set; }

    public FinalList(List<object> values)
    {
        Values = values;
    }

    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < Values.Count; i++)
        {
            s += Values[i].ToString() + " ";
        }
        return s;
    }
}
