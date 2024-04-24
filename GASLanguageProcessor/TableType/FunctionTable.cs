using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class FunctionTable: ITable<FunctionType>
{
    public FunctionTable()
    {
        Functions.Add("Colour", new FunctionType(GasType.Colour, [ GasType.Number, GasType.Number, GasType.Number, GasType.Number ]));
        Functions.Add("Point", new FunctionType(GasType.Point, [ GasType.Number, GasType.Number ]));
        Functions.Add("Square", new FunctionType(GasType.Square, [ GasType.Point, GasType.Number, GasType.Colour, GasType.Colour]));
        Functions.Add("Circle", new FunctionType(GasType.Circle, [GasType.Point, GasType.Number, GasType.Number, GasType.Colour, GasType.Colour]));
        Functions.Add("Line", new FunctionType(GasType.Line, [GasType.Point, GasType.Point, GasType.Number, GasType.Colour]));
        Functions.Add("Rectangle", new FunctionType(GasType.Rectangle, [GasType.Point, GasType.Number, GasType.Number, GasType.Colour, GasType.Colour]));
        Functions.Add("Text", new FunctionType(GasType.Text, [GasType.Point, GasType.String, GasType.Number, GasType.String, GasType.Colour]));
    }

    public Dictionary<string, FunctionType> Functions { get; protected set; } = new();

    public void Add(string key, FunctionType value)
    {
        Functions.Add(key, value);
    }

    public FunctionType Get(string key)
    {
        return Functions[key];
    }

    public bool Contains(string key)
    {
        return Functions.ContainsKey(key);
    }

    public void Remove(string key)
    {
        Functions.Remove(key);
    }

    public void Clear()
    {
        Functions.Clear();
    }
}