using System.Collections;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalList
{
    public List<object> Values { get; set; }
    public Scope Scope { get; set; }

    public FinalList(List<object> values, Scope scope)
    {
        Values = values;
        Scope = scope;
    }
}