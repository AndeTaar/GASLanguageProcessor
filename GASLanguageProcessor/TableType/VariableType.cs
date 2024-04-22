using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class VariableType
{
    public GasType Type { get; protected set; }
    public AstNode Value { get; protected set; }

    public VariableType(GasType type, AstNode value)
    {
        Type = type;
        Value = value;
    }
}