namespace GASLanguageProcessor.TableType;

public class Struct
{
    public Dictionary<string, Object> Values { get; set; } = new();

    public Struct(Dictionary<string, Object> values)
    {
        Values = values;
    }
}
