using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;

namespace GASLanguageProcessor.FinalTypes;

public class FinalRecord : FinalType
{
    public string FinalRecordType { get; set; }

    public FinalRecord(string finalRecordType)
    {
        FinalRecordType = finalRecordType;
    }
}