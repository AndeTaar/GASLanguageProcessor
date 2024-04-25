using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class FunctionTable
{
    public Dictionary<string, FunctionType> Functions { get; protected set; } = new();


    public void Bind(string key, FunctionType value)
    {
        Functions.Add(key, value);
    }

    public FunctionType? LookUp(string key)
    {
        return Functions[key];
    }
}