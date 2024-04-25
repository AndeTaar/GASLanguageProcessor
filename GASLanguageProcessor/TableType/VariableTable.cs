namespace GASLanguageProcessor.TableType;

public class VariableTable
{
    public Dictionary<string, VariableType> Variables { get; protected set; } = new();

    public Scope Scope { get; set; }

    public VariableTable(Scope scope)
    {
        this.Scope = scope;
    }

    public void Bind(string key, VariableType value)
    {
        Variables.Add(key, value);
    }

    public VariableType? LookUp(string key)
    {
        if (Variables.ContainsKey(key))
        {
            return Variables[key];
        }
        return this.Scope.ParentScope?.vTable.LookUp(key);
    }

}