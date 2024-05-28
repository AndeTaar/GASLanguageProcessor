namespace GASLanguageProcessor.TableType;

public class Store
{
    public Dictionary<int, object> Values { get; set; } = new();

    public void Bind(int key, object value)
    {
        Values[key] = value;
    }

    public object? LookUp(int key)
    {
        if (Values.ContainsKey(key))
        {
            return Values[key];
        }
        return null;
    }
}
