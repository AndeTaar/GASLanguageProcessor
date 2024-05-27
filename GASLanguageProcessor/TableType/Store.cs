namespace GASLanguageProcessor.TableType;

public class Store
{
    public Dictionary<int, Variable> Values { get; set; } = new();
    public Scope Scope { get; set; }

    public Store(Scope scope)
    {
        this.Scope = scope;
    }

    public bool Bind(int index, Variable value)
    {
        if (Values.ContainsKey(index))
        {
            return false;
        }
        Values.Add(index, value);
        return true;
    }

    public int AddNewValue(Variable value)
    {
        int maxIndex = Values.Keys.Max();
        Values.Add(maxIndex, value);
        return maxIndex;
    }

    public Variable LookUp(int index)
    {
        if (Values.ContainsKey(index))
        {
            return Values[index];
        }

        return null;
    }

    public Variable? LocalLookUp(int index)
    {
        if (Values.ContainsKey(index))
        {
            return Values[index];
        }
        return null;
    }
}
