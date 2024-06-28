namespace CARLLanguageProcessor.TableType;

public class VarEnv
{
    public VarEnv(VarEnv? parent = null)
    {
        Parent = parent;
    }

    public Dictionary<string, int> Variables { get; set; } = new();
    public VarEnv? Parent { get; set; }
    public static int next { get; set; }

    public int GetNext()
    {
        return ++next;
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
        if (Variables.ContainsKey(key)) return false;
        Variables.Add(key, index);
        return true;
    }

    public string GetIdentifier(int key)
    {
        return Variables.FirstOrDefault(x => x.Value == key).Key ?? "";
    }

    public int? LookUp(string key)
    {
        if (Variables.ContainsKey(key)) return Variables[key];
        return Parent?.LookUp(key);
    }

    public int? LocalLookUp(string key)
    {
        if (Variables.ContainsKey(key)) return Variables[key];
        return null;
    }
}
