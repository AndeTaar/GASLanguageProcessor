namespace GASLanguageProcessor.TableType;

public class FunctionTable
{
    public Dictionary<string, FunctionType> Functions { get; protected set; } = new();

    public Scope Scope { get; set; }

    public FunctionTable(Scope scope)
    {
        this.Scope = scope;
    }

    public void Bind(string key, FunctionType value)
    {
        Functions.Add(key, value);
    }

    public FunctionType? LookUp(string key)
    {
        if (Functions.ContainsKey(key))
        {
            return Functions[key];
        }
        return this.Scope.ParentScope?.fTable.LookUp(key);
    }
}