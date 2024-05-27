using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class TypeTable
{
public Dictionary<string, GasType> Types { get; set; } = new();

    public Scope Scope { get; set; }

    public TypeTable(Scope scope)
    {
        this.Scope = scope;
    }

    public bool Bind(string key, GasType value)
    {
        if(Types.ContainsKey(key))
        {
            return false;
        }
        Types.Add(key, value);
        return true;
    }

    public GasType? LookUp(string key)
    {
        if (Types.ContainsKey(key))
        {
            return Types[key];
        }
        return this.Scope.ParentScope?.tTable.LookUp(key);
    }

}