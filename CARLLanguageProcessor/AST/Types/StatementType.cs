namespace CARLLanguageProcessor.AST.Types.StatementsType;

public class StatementType: CARLType
{
    public StatementTypes Type { get; set; }

    public StatementType(StatementTypes type)
    {
        Type = type;
    }

    public override string ToString()
    {
        return Type.ToString();
    }

    public override bool Equals(CARLType other)
    {
        return other is StatementType statementType && statementType.Type == Type;
    }
}

