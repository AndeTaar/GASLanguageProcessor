using CARLLanguageProcessor.AST.Expressions.Terms.Identifiers;

namespace CARLLanguageProcessor.FinalTypes;

public class FinalRecord : FinalType
{
    public string FinalRecordType { get; set; }

    public FinalRecord(string finalRecordType)
    {
        FinalRecordType = finalRecordType;
    }
}
