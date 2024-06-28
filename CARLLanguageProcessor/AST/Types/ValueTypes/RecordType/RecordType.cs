namespace CARLLanguageProcessor.AST.Types.RecordType;

public class RecordType: ValueType
{
    public CARLRecordTypes Type { get; set; }

    public RecordType(CARLRecordTypes type)
    {
        Type = type;
    }

    public override string ToString()
    {
        return Type.ToString();
    }

    public override bool Equals(CARLType other)
    {
        return other is RecordType recordType && recordType.Type == Type;
    }
}
