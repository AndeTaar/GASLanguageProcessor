using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class Variable
{
    public string Identifier { get; set; }
    public GasType Type { get; set; }
    public Expression FormalValue { get; set; }
    public object ActualValue { get; set; }

    public Variable(string identifier, GasType type, Expression? formalValue = null)
    {
        Identifier = identifier;
        Type = type;
        FormalValue = formalValue;
    }

    public Variable(string identifier, GasType type, object actualValue)
    {
        Identifier = identifier;
        Type = type;
        ActualValue = actualValue;
    }
}
