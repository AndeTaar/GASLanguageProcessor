namespace GASLanguageProcessor.AST.Types.RecordType;

public class RecordType: ValueType
{
    public GasRecordTypes Type { get; set; }

    public RecordType(GasRecordTypes type)
    {
        Type = type;
    }

    public override string ToString()
    {
        return Type.ToString();
    }

    public override bool Equals(GasType other)
    {
        return other is RecordType recordType && recordType.Type == Type;
    }
}
