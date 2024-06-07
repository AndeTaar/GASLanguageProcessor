using GASLanguageProcessor.FinalTypes;

namespace GASLanguageProcessor.TableType;

public class RecEnv
{
    public Dictionary<string, (FinalRecord, VarEnv, FuncEnv, RecEnv, Store)> Records { get; set; } = new();


    public RecEnv? Parent { get; set; }

    public RecEnv(RecEnv? parent = null)
    {
        Parent = parent;
    }

    public RecEnv EnterScope()
    {
        return new RecEnv(this);
    }

    public RecEnv ExitScope()
    {
        return Parent ?? this;
    }

    public bool Bind(string key, (FinalRecord, VarEnv, FuncEnv, RecEnv, Store) value)
    {
        if (Records.ContainsKey(key)) return false;
        Records.Add(key, value);
        return true;
    }

    public (FinalRecord, VarEnv, FuncEnv, RecEnv, Store)? LookUp(string key)
    {
        if (Records.ContainsKey(key)) return Records[key];
        return Parent?.LookUp(key);
    }


}
