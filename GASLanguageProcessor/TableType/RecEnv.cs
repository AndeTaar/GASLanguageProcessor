namespace GASLanguageProcessor.TableType;

public class RecEnv
{
    public Dictionary<string, (VarEnv, RecEnv, Store)> RecordTypes { get; set; } = new();
}