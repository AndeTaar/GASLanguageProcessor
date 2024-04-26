using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class Scope
{
    public Scope? ParentScope { get; set; }
    public AstNode? ScopeNode { get; set; }

    public List<Scope> Scopes { get; set; } = new();

    public FunctionTable fTable { get; set; }

    public VariableTable vTable { get; set; }

    public Scope(Scope? parentScope, AstNode? node)
    {
        fTable = new FunctionTable(this);
        vTable = new VariableTable(this);

        ParentScope = parentScope;

        ScopeNode = node;

        if (node != null)
        {
            node.Scope = this;
        }

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

    public Scope EnterScope(AstNode node)
    {
        var scope = new Scope(this, node);
        Scopes.Add(scope);
        return scope;
    }

    public Scope ExitScope()
    {
        return ParentScope ?? throw new Exception("Cannot exit global scope");;
    }


}
