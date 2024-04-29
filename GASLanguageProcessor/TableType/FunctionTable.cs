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

    public void Bind(string key, Function value)
    {
        Functions.Add(key, value);
    }

    public void SetReturnType(string key, GasType type)
    {
        if (Functions.ContainsKey(key))
        {
            Functions[key].ReturnType = type;
        }
    }

    public void SetParameterTypes(string key, List<GasType> parameters)
    {
        if (Functions.ContainsKey(key))
        {
            for (int i = 0; i < Functions[key].Parameters.Count; i++)
            {
                Functions[key].Parameters[i].Type = parameters[i];
            }
        }
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