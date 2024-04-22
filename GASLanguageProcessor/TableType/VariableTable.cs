using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class VariableTable: ITable<GasType>
{
    public Dictionary<string, GasType> Variables { get; protected set; } = new();

    public void Add(string key, GasType value)
    {
        Variables.Add(key, value);
    }

    public GasType Get(string key)
    {
        return Variables[key];
    }

    public bool Contains(string key)
    {
        return Variables.ContainsKey(key);
    }

    public void Remove(string key)
    {
        Variables.Remove(key);
    }

    public void Clear()
    {
        Variables.Clear();
    }

}