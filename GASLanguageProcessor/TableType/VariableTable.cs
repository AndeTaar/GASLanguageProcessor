namespace GASLanguageProcessor.TableType;

public class VariableTable: ITable<VariableType>
{
    public Dictionary<string, VariableType> Variables { get; protected set; } = new();

    public void Add(string key, VariableType value)
    {
        Variables.Add(key, value);
    }

    public VariableType Get(string key)
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