namespace GASLanguageProcessor.TableType;

public class VariableTable
{
    public static VariableTable? GlobalScope { get; set; }

    public VariableTable ParentScope { get; set; }

    public Dictionary<string, VariableType> Variables { get; protected set; } = new();

    public static List<VariableTable> Scopes { get; set; } = new();

    public VariableTable()
    {
        if(GlobalScope == null)
        {
            GlobalScope = this;
        }
    }


    public void Bind(string key, VariableType value)
    {
        Variables.Add(key, value);
    }

    public VariableType? LookUp(string key)
    {
        return Variables[key];
    }

}