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
            fTable.Bind("Colour", new Function(GasType.Colour, new List<Variable>()
                {
                    new Variable("red", this, GasType.Number),
                    new Variable("green", this, GasType.Number),
                    new Variable("blue", this, GasType.Number),
                    new Variable("alpha", this, GasType.Number)
                },
                new Return(new Colour(
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
                    new Variable("topLeft", this, GasType.Point),
                    new Variable("bottomRight", this, GasType.Point),
                    new Variable("stroke", this, GasType.Number),
                    new Variable("colour", this, GasType.Colour),
                    new Variable("strokeColour", this, GasType.Colour)
                },
                new Return(new Rectangle(
                    new Identifier("topLeft"),
                    new Identifier("bottomRight"),
                    new Identifier("stroke"),
                    new Identifier("colour"),
                    new Identifier("strokeColour"))),
                new Scope(this, null)));

            fTable.Bind("Text", new Function(GasType.Text, new List<Variable>()
            {
                new Variable("value", this, GasType.String),
                new Variable("position", this, GasType.Point),
                new Variable("font", this, GasType.String),
                new Variable("fontSize", this, GasType.Number),
                new Variable("colour", this, GasType.Colour)
            }, new Return(new Text(
                new Identifier("value"),
                new Identifier("position"),
                new Identifier("font"),
                new Identifier("fontSize"),
                new Identifier("colour")
            )), new Scope(this, null)));

            fTable.Bind("Line", new Function(GasType.Line, new List<Variable>()
            {
                new Variable("start", this, GasType.Point),
                new Variable("end", this, GasType.Point),
                new Variable("stroke", this, GasType.Number),
                new Variable("colour", this, GasType.Colour)
            }, new Return(new Line(
                new Identifier("start"),
                new Identifier("end"),
                new Identifier("stroke"),
                new Identifier("colour")
            )), new Scope(this, null)));

            fTable.Bind("Square", new Function(GasType.Square, new List<Variable>()
            {
                new Variable("topLeft", this, GasType.Point),
                new Variable("length", this, GasType.Number),
                new Variable("stroke", this, GasType.Number),
                new Variable("colour", this, GasType.Colour),
                new Variable("strokeColour", this, GasType.Colour)
            }, new Return(new Square(
                new Identifier("topLeft"),
                new Identifier("length"),
                new Identifier("stroke"),
                new Identifier("colour"),
                new Identifier("strokeColour")
            )), new Scope(this, null)));

            fTable.Bind("Circle", new Function(GasType.Circle, new List<Variable>()
            {
                new Variable("center", this, GasType.Point),
                new Variable("radius", this, GasType.Number),
                new Variable("stroke", this, GasType.Number),
                new Variable("colour", this, GasType.Colour),
                new Variable("strokeColour", this, GasType.Colour)
            }, new Return(new Circle(
                new Identifier("center"),
                new Identifier("radius"),
                new Identifier("stroke"),
                new Identifier("colour"),
                new Identifier("strokeColour")
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
