using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class VarEnv
{
    public Dictionary<string, int> Variables { get; set; } = new();
    public VarEnv? Parent { get; set; }
    public int next = 0;

    public VarEnv(VarEnv? parent = null)
    {
        this.Parent = parent;
    }

    public VarEnv EnterScope()
    {
        return new VarEnv(this);
    }

    public VarEnv ExitScope()
    {
        return Parent ?? this;
    }

    public bool Bind(string key, int index)
    {
        if (Variables.ContainsKey(key))
        {
            return false;
        }
        Variables.Add(key, index);
        return true;
    }

    public string GetIdentifier(int key)
    {
        return Variables.FirstOrDefault(x => x.Value == key).Key ?? "";
    }

    public int? LookUp(string key)
    {
        if (Variables.ContainsKey(key))
        {
            return Variables[key];
        }
        return this.Parent?.LookUp(key);
    }

    public int? LocalLookUp(string key)
    {
        if (Variables.ContainsKey(key))
        {
            return Variables[key];
        }
        return null;
    }

}
