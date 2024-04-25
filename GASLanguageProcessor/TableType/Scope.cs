using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class Scope
{
    public Scope? ParentScope { get; set; }

    public List<Scope> Scopes { get; set; } = new();

    public FunctionTable fTable { get; set; }

    public VariableTable vTable { get; set; }

    /**
     *  Functions.Add("Colour", new FunctionType(GasType.Colour, [ GasType.Number, GasType.Number, GasType.Number, GasType.Number ]));
        Functions.Add("Point", new FunctionType(GasType.Point, [ GasType.Number, GasType.Number ]));
        Functions.Add("Square", new FunctionType(GasType.Square, [ GasType.Point, GasType.Number, GasType.Colour, GasType.Colour]));
        Functions.Add("Circle", new FunctionType(GasType.Circle, [GasType.Point, GasType.Number, GasType.Number, GasType.Colour, GasType.Colour]));
        Functions.Add("Line", new FunctionType(GasType.Line, [GasType.Point, GasType.Point, GasType.Number, GasType.Colour]));
        Functions.Add("Rectangle", new FunctionType(GasType.Rectangle, [GasType.Point, GasType.Number, GasType.Number, GasType.Colour, GasType.Colour]));
        Functions.Add("Text", new FunctionType(GasType.Text, [GasType.Point, GasType.String, GasType.Number, GasType.String, GasType.Colour]));

        if(GlobalScope == null)
        {
            GlobalScope = this;
        }
     */

    public Scope(Scope? parentScope)
    {
        fTable = new FunctionTable();
        vTable = new VariableTable();

        ParentScope = parentScope;

        if (ParentScope == null)
        {
            fTable.Bind("Colour",
                new FunctionType(GasType.Colour,
                    new List<GasType> { GasType.Number, GasType.Number, GasType.Number, GasType.Number }));
            fTable.Bind("Point",
                new FunctionType(GasType.Point, new List<GasType> { GasType.Number, GasType.Number }));
            fTable.Bind("Square",
                new FunctionType(GasType.Square, new List<GasType> { GasType.Point, GasType.Number, GasType.Colour, GasType.Colour }));
            fTable.Bind("Circle",
                new FunctionType(GasType.Circle, new List<GasType> { GasType.Point, GasType.Number, GasType.Number, GasType.Colour, GasType.Colour }));
            fTable.Bind("Line",
                new FunctionType(GasType.Line, new List<GasType> { GasType.Point, GasType.Point, GasType.Number, GasType.Colour }));
            fTable.Bind("Rectangle",
                new FunctionType(GasType.Rectangle, new List<GasType> { GasType.Point, GasType.Number, GasType.Number, GasType.Colour, GasType.Colour }));
            fTable.Bind("Text",
                new FunctionType(GasType.Text, new List<GasType> { GasType.Point, GasType.String, GasType.Number, GasType.String, GasType.Colour }));
        }
    }

    public FunctionType? GlobalLookupFunction(string key)
    {
        if (fTable.LookUp(key) != null)
        {
            return fTable.LookUp(key);
        }

        return ParentScope?.GlobalLookupFunction(key);
    }

    public VariableType? GlobalLookupVariable(string key)
    {
        if (vTable.LookUp(key) != null)
        {
            return vTable.LookUp(key);
        }

        return ParentScope?.GlobalLookupVariable(key);
    }

    public Scope EnterScope()
    {
        var scope = new Scope(this);
        Scopes.Add(scope);
        return scope;
    }

    public Scope ExitScope()
    {
        return ParentScope ?? throw new Exception("Cannot exit global scope");;
    }


}