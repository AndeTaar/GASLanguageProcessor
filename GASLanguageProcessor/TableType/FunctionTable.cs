using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class FunctionTable
{
    public Dictionary<string, Function> Functions { get; protected set; } = new();

    public Scope Scope { get; set; }

    public FunctionTable(Scope scope)
    {
        this.Scope = scope;
    }

    public bool Bind(string key, Function value)
    {
        if(Functions.ContainsKey(key))
        {
            return false;
        }
        Functions.Add(key, value);
        return true;
    }

    public Function? LookUp(string key)
    {
        if (Functions.ContainsKey(key))
        {
            return Functions[key];
        }
        return this.Scope.ParentScope?.fTable.LookUp(key);
    }
}
