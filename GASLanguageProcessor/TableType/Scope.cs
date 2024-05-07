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

    public CollectionTable cTable { get; set; }

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
            fTable.Bind("Color", new Function(GasType.Color, new List<Variable>()
                {
                    new Variable("red", this, GasType.Number),
                    new Variable("green", this, GasType.Number),
                    new Variable("blue", this, GasType.Number),
                    new Variable("alpha", this, GasType.Number)
                },
                new Return(new Color(
                    new Identifier("red"),
                    new Identifier("green"),
                    new Identifier("blue"),
                    new Identifier("alpha"))),
                new Scope(this, null)));

            fTable.Bind("Point", new Function(GasType.Point, new List<Variable>()
                {
                    new Variable("x", this, GasType.Number),
                    new Variable("y", this, GasType.Number)
                },
                new Return(new Point(
                    new Identifier("x"),
                    new Identifier("y"))),
                new Scope(this, null)));

            fTable.Bind("Rectangle", new Function(GasType.Rectangle, new List<Variable>()
                {
                    new Variable("topLeft", GasType.Point),
                    new Variable("bottomRight", GasType.Point),
                    new Variable("stroke", GasType.Number),
                    new Variable("color", GasType.Color),
                    new Variable("strokeColor", GasType.Color)
                },
                new Return(new Rectangle(
                    new Identifier("topLeft"),
                    new Identifier("bottomRight"),
                    new Identifier("stroke"),
                    new Identifier("color"),
                    new Identifier("strokeColor"))),
                new Scope(this, null)));

            fTable.Bind("Text", new Function(GasType.Text, new List<Variable>()
            {
                new Variable("value", GasType.String),
                new Variable("position", GasType.Point),
                new Variable("font", GasType.String),
                new Variable("fontSize", GasType.Number),
                new Variable("color", GasType.Color)
            }, new Return(new Text(
                new Identifier("value"),
                new Identifier("position"),
                new Identifier("font"),
                new Identifier("fontSize"),
                new Identifier("color")
            )), new Scope(this, null)));

            fTable.Bind("Line", new Function(GasType.Line, new List<Variable>()
            {
                new Variable("intercept", GasType.Number),
                new Variable("gradient", GasType.Number),
                new Variable("stroke", GasType.Number),
                new Variable("color", GasType.Color)
            }, new Return(new Line(
                new Identifier("intercept"),
                new Identifier("gradient"),
                new Identifier("stroke"),
                new Identifier("color")
            )), new Scope(this, null)));

            fTable.Bind("SegLine", new Function(GasType.SegLine, new List<Variable>()
            {
                new Variable("start", GasType.Point),
                new Variable("end", GasType.Point),
                new Variable("stroke", GasType.Number),
                new Variable("color", GasType.Color)
            }, new Return(new SegLine(
                new Identifier("start"),
                new Identifier("end"),
                new Identifier("stroke"),
                new Identifier("color")
            )), new Scope(this, null)));

            fTable.Bind("Square", new Function(GasType.Square, new List<Variable>()
            {
                new Variable("topLeft", GasType.Point),
                new Variable("length", GasType.Number),
                new Variable("stroke", GasType.Number),
                new Variable("color", GasType.Color),
                new Variable("strokeColor", GasType.Color)
            }, new Return(new Square(
                new Identifier("topLeft"),
                new Identifier("length"),
                new Identifier("stroke"),
                new Identifier("color"),
                new Identifier("strokeColor")
            )), new Scope(this, null)));

            fTable.Bind("Circle", new Function(GasType.Circle, new List<Variable>()
            {
                new Variable("center", GasType.Point),
                new Variable("radius", GasType.Number),
                new Variable("stroke", GasType.Number),
                new Variable("color", GasType.Color),
                new Variable("strokeColor", GasType.Color)
            }, new Return(new Circle(
                new Identifier("center"),
                new Identifier("radius"),
                new Identifier("stroke"),
                new Identifier("color"),
                new Identifier("strokeColor")
            )), new Scope(this, null)));

            fTable.Bind("Ellipse", new Function(GasType.Ellipse, new List<Variable>()
            {
                new Variable("center", GasType.Point),
                new Variable("xRadius", GasType.Number),
                new Variable("yRadius", GasType.Number),
                new Variable("color", GasType.Color),
                new Variable("borderColor", GasType.Color),
                new Variable("borderWidth", GasType.Number)
            }, new Return(new Ellipse(
                new Identifier("center"),
                new Identifier("xRadius"),
                new Identifier("yRadius"),
                new Identifier("color"),
                new Identifier("borderColor"),
                new Identifier("borderWidth")
            )), new Scope(this, null)));
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

    public void AddListMethods()
    {
        this.fTable.Bind("Add", new Function(GasType.Void, new List<Variable>()
        {
            new Variable("value", this, GasType.Any)
        }, new Return(new AddToList(new Identifier("value"))), new Scope(this, null)));
    }


}
