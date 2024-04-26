using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class VariableType
{
    public GasType Type { get; protected set; }
    public Expression Value { get; protected set; }

    public VariableType(GasType type, Expression value)
    {
        Type = type;
        Value = value;
    }
}
