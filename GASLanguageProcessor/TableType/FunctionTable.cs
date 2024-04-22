using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class FunctionTable: ITable<FunctionType>
{
    public FunctionTable()
    {
        Functions.Add("Colour", new FunctionType(GasType.Colour, [ GasType.Number, GasType.Number, GasType.Number, GasType.Number ]));
    }

    public Dictionary<string, FunctionType> Functions { get; protected set; } = new();

    public void Add(string key, FunctionType value)
    {
        Functions.Add(key, value);
    }

    public FunctionType Get(string key)
    {
        return Functions[key];
    }

    public bool Contains(string key)
    {
        return Functions.ContainsKey(key);
    }

    public void Remove(string key)
    {
        Functions.Remove(key);
    }

    public void Clear()
    {
        Functions.Clear();
    }
}