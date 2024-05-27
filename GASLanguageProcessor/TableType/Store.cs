namespace GASLanguageProcessor.TableType;

public class Store
{
    public Store? Parent { get; set; }
    public Dictionary<int, object> Values { get; set; } = new();

    public Store EnterScope()
    {
        return new Store(this);
    }

    public void ExitScope()
    {
        Parent = Parent?.Parent;
    }

    public Store(Store? parent = null)
    {
        Parent = parent;
    }

    public void Bind(int key, object value)
    {
        Values[key] = value;
    }

    public object? LookUp(int key)
    {
        return Values[key];
    }
}
