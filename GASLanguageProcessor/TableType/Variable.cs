using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class Variable
{
    public string Identifier { get; set; }
    public GasType Type { get; set; }
    public Expression Expression { get; set; }

    public Variable(string identifier, GasType type, Expression? expression = null)
    {
        Identifier = identifier;
        Type = type;
        Expression = expression;
    }
}
