using System.Linq.Expressions;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class Collection
{
    public string Identifier { get; set; }
    public GasType Type { get; set; }
    public List<Expression> FormalValues { get; set; }
    public List<object> ActualValues { get; set; }
    
    public Collection(string identifier, List<Expression> formalValues, GasType type)
    {
        Identifier = identifier;
        FormalValues = formalValues;
        Type = type;
    }
    
    public Collection(string identifier, List<object> actualValues)
    {
        Identifier = identifier;
        ActualValues = actualValues;
    }
}