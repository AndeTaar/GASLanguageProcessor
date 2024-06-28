namespace CARLLanguageProcessor.TableType;

public class RecTypeEnv
{
    public Dictionary<string, (VarEnv, RecEnv, Store)> RecordTypes { get; set; } = new();
}
