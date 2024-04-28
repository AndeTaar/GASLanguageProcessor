using System;
using System.Collections.Generic;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
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
                new Function(GasType.Colour,
                    new List<Variable> { new Variable("red", GasType.Number),  new Variable("green", GasType.Number),
                        new Variable("blue", GasType.Number),  new Variable("alpha", GasType.Number) },
                    new Compound(new Return(new Colour(
                        new Identifier("red"),
                        new Identifier("green"),
                        new Identifier("blue"),
                        new Identifier("alpha")
                    }))));
            fTable.Bind("Point",
                new Function(GasType.Point, new List<GasType> { GasType.Number, GasType.Number }));
            fTable.Bind("Square",
                new Function(GasType.Square, new List<GasType> { GasType.Point, GasType.Number, GasType.Colour, GasType.Colour }));
            fTable.Bind("Circle",
                new Function(GasType.Circle, new List<GasType> { GasType.Point, GasType.Number, GasType.Number, GasType.Colour, GasType.Colour }));
            fTable.Bind("Line",
                new Function(GasType.Line, new List<GasType> { GasType.Point, GasType.Point, GasType.Number, GasType.Colour }));
            fTable.Bind("Rectangle",
                new Function(GasType.Rectangle, new List<GasType> { GasType.Point, GasType.Number, GasType.Number, GasType.Colour, GasType.Colour }));
            fTable.Bind("Text",
                new Function(GasType.Text, new List<GasType> { GasType.Point, GasType.String, GasType.Number, GasType.String, GasType.Colour }));
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
