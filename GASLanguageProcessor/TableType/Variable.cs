using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class Variable
{
    public string Identifier { get; set; }
    public GasType Type { get; set; }
    public Expression FormalValue { get; set; }
    public object ActualValue { get; set; }

    public Variable(string identifier, Expression? formalValue = null)
    {
        Identifier = identifier;
        FormalValue = formalValue;
    }

    public Variable(string identifier, object actualValue)
    {
        Identifier = identifier;
        ActualValue = actualValue;
    }

    public Variable(string identifier, GasType type)
    {
        Identifier = identifier;
        Type = type;
    }
}
