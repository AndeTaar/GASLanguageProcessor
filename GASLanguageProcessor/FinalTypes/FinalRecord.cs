using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;

namespace GASLanguageProcessor.FinalTypes;

public class FinalRecord: FinalType
{
    public FinalRecord(List<Identifier> identifiers, List<object> values)
    {
        Fields = new();
        for (int i = 0; i < identifiers.Count; i++)
        {
            Fields[identifiers[i].Name] = values[i];
        }
    }

    public FinalRecord(Dictionary<string, object> fields)
    {
        Fields = fields;
    }
}
