using System.Runtime.CompilerServices;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor;

public class Scope 
{
    private static List<Scope> Scopes { get; } = new();
    public Scope? Parent { get; protected set; }
    public VariableTable Variables { get; protected set; } = new();
    public FunctionTable Functions { get; protected set; } = new();
    
    public Scope(Scope parent)
    {
        Parent = parent;
        Scopes.Add(this);
    }
    
    // Perhaps unnecessary to redefine Scopes.Last() as a method for readability
    public static Scope MostRecent()
    {
        return Scopes.Last(); 
    }
    
    // Checking if the current Scope OR any of its parents contain the key for this function
    public bool FtableContains(string key)
    {
        return Parent == null ? Functions.Contains(key) : Functions.Contains(key) || Parent.FtableContains(key);
    }
    
    public bool VtableContains(string key)
    {
        return Parent == null ? Variables.Contains(key) : Variables.Contains(key) || Parent.VtableContains(key);
    }
    
    // Retrieves the variable from the current scope OR any of its parents
    public VariableType GetVariable(string key)
    {
        if (Parent != null)
        {
            return Variables.Contains(key) ? Variables.Get(key) : Parent.GetVariable(key);
        }
        
        throw new System.Exception("Variable not found");
    }
    
    // Retrieves the function from the current scope OR any of its parents
    public FunctionType GetFunction(string key)
    {
        if (Parent != null)
        {
            return Functions.Contains(key) ? Functions.Get(key) : Parent.GetFunction(key);
        }
        
        throw new System.Exception("Function not found");
    }
}