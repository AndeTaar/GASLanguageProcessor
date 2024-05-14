using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class VariableTable
{
    public Dictionary<string, Variable> Variables { get; set; } = new();

    public Scope Scope { get; set; }

    public VariableTable(Scope scope)
    {
        this.Scope = scope;
    }

    public bool Bind(string key, Variable value)
    {
        if (Variables.ContainsKey(key))
        {
            return false;
        }
        Variables.Add(key, value);
        return true;
    }

    public Variable? LookUp(string key)
    {
        if (Variables.ContainsKey(key))
        {
            return Variables[key];
        }
        return this.Scope.ParentScope?.vTable.LookUp(key);
    }

    public Variable? LocalLookUp(string key)
    {
        if (Variables.ContainsKey(key))
        {
            return Variables[key];
        }
        return null;
    }

}
