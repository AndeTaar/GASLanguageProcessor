using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;

namespace GASLanguageProcessor.FinalTypes;

public class FinalRecord : FinalType
{
    public List<FinalType> FinalTypes { get; }

    public FinalRecord(List<FinalType> finalTypes)
    {
        FinalTypes = finalTypes;
    }
}