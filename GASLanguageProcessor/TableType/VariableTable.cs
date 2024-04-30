using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class VariableTable
{
    public Dictionary<string, Variable> Variables { get; protected set; } = new();

    public Scope Scope { get; set; }

    public VariableTable(Scope scope)
    {
        this.Scope = scope;
    }

    public void Bind(string key, Variable value)
    {
        Variables.Add(key, value);
    }

    public void SetType(string key, GasType type)
    {
        Variables[key].Type = type;
    }

    public Variable? LookUp(string key)
    {
        if (Variables.ContainsKey(key))
        {
            return Variables[key];
        }
        return this.Scope.ParentScope?.vTable.LookUp(key);
    }

}