using GASLanguageProcessor.FinalTypes;

namespace GASLanguageProcessor.TableType;

public class RecEnv
{
    public Dictionary<string, (FuncEnv, RecEnv)> RecordTypes { get; set; } = new();

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

    public bool TypeBind(string key, (FuncEnv, RecEnv) value)
    {
        if (RecordTypes.ContainsKey(key)) return false;
        RecordTypes.Add(key, value);
        return true;
    }

    public (FuncEnv, RecEnv)? TypeLookUp(string key)
    {
        if (RecordTypes.ContainsKey(key)) return RecordTypes[key];
        return Parent?.TypeLookUp(key);
    }


}
