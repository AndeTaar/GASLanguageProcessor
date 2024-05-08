using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class CollectionTable
{
    public Dictionary<string, Collection> Lists { get; protected set; } = new();

    public Scope Scope { get; set; }

    public CollectionTable(Scope scope)
    {
        this.Scope = scope;
    }

    public void Bind(string key, Collection value)
    {
        Lists.Add(key, value);
    }
    

    public Collection? ListLookUp(string key)
    {
        if (Lists.ContainsKey(key))
        {
            return Lists[key];
        }

        return this.Scope.ParentScope?.cTable.ListLookUp(key);
    }
}