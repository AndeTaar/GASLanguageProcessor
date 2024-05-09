using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class Variable
{
    public string Identifier { get; set; }
    public GasType Type { get; set; }
    public Expression FormalValue { get; set; }
    public object ActualValue { get; set; }
    public Scope Scope { get; set; }

    public Variable(string identifier, Scope scope, GasType type, Expression? formalValue = null)
    {
        Identifier = identifier;
        FormalValue = formalValue;
        Type = type;
        Scope = scope;
    }

    public Variable(string identifier, object actualValue)
    {
        Identifier = identifier;
        ActualValue = actualValue;
    }

    public Variable(string identifier, Scope scope,  GasType type)
    {
        Identifier = identifier;
        Type = type;
        Scope = scope;
    }
}
