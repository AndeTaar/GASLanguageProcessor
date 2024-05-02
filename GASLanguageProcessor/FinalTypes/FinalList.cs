using System.Collections;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalList
{
    public string Identifier { get; set; }
    public  List<object> Values { get; set; }
    public Scope Scope { get; set; }
    
    public FinalList(string identifier, List<object> values, Scope scope)
    {
        Identifier = identifier;
        Values = values;
        Scope = scope;
    }
}