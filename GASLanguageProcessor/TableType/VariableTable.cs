using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class VariableTable
{
    public Dictionary<string, int?> Variables { get; set; } = new();

    public Scope Scope { get; set; }

    public VariableTable(Scope scope)
    {
        this.Scope = scope;
    }

    public bool Bind(string key, int index)
    {
        if (Variables.ContainsKey(key))
        {
            return false;
        }
        Variables.Add(key, index);
        return true;
    }
    public bool Bind(string key)
    {
        if (Variables.ContainsKey(key))
        {
            return false;
        }
        Variables.Add(key, null);
        return true;
    }

    public int? LookUp(string key)
    {
        if (Variables.ContainsKey(key))
        {
            return Variables[key];
        }
        return this.Scope.ParentScope?.vTable.LookUp(key);
    }

    public int? LocalLookUp(string key)
    {
        if (Variables.ContainsKey(key))
        {
            return Variables[key];
        }
        return null;
    }

}
