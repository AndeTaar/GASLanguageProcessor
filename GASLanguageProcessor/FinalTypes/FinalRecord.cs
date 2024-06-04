using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;

namespace GASLanguageProcessor.FinalTypes;

public class FinalRecord
{
    public string Type { get; set; }
    public Dictionary<string, object> Fields { get; set; }

    public FinalRecord(string type, List<Identifier> identifiers, List<object> values)
    {
        Type = type;
        Fields = new();
        for (int i = 0; i < identifiers.Count; i++)
        {
            Fields[identifiers[i].Name] = values[i];
        }
    }
}
