using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class CollectionTable
{
    public Dictionary<string, List> Lists { get; protected set; } = new();
    public Dictionary<string, Group> Groups { get; protected set; } = new();

    public Scope Scope { get; set; }

    public CollectionTable(Scope scope)
    {
        this.Scope = scope;
    }

    public void Bind(string key, Group value)
    {
        Groups.Add(key, value);
    }
    
    public void Bind(string key, List value)
    {
        Lists.Add(key, value);
    }

    // Lists need types, Group members probably also need types 
    public void SetType(string key, GasType type)
    {
        Lists[key].Type = type;
    }
    
    
    //This way of LookUp allows for Lists and Groups with the same identifiers, finurlig
    public List? ListLookUp(string key)
    {
        if (Lists.ContainsKey(key))
        {
            return Lists[key];
        }
        return this.Scope.ParentScope?.cTable.ListLookUp(key);
    }
    
    public Group? GroupLookUp(string key)
    {
        if (Groups.ContainsKey(key))
        {
            return Groups[key];
        }
        return this.Scope.ParentScope?.cTable.GroupLookUp(key);
    }
}