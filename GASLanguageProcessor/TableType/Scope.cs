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
                    new List<Variable>
                    {
                        new Variable("red", GasType.Number), new Variable("green", GasType.Number),
                        new Variable("blue", GasType.Number), new Variable("alpha", GasType.Number)
                    },
                    new Return(new Colour(
                        new Identifier("red"),
                        new Identifier("green"),
                        new Identifier("blue"),
                        new Identifier("alpha"))),
                this));

            fTable.Bind("Point", new Function(GasType.Point, new List<Variable>()
                {
                    new Variable("x", GasType.Number),
                    new Variable("y", GasType.Number)
                },
                new Return(new Point(
                    new Identifier("x"),
                    new Identifier("y"))),
                this));
            fTable.Bind("Rectangle", new Function(GasType.Rectangle, new List<Variable>()
                {
                    new Variable("topLeft", GasType.Point),
                    new Variable("bottomRight", GasType.Point),
                    new Variable("stroke", GasType.Number),
                    new Variable("colour", GasType.Colour),
                    new Variable("strokeColour", GasType.Colour)
                },
                new Return(new Rectangle(
                    new Identifier("topLeft"),
                    new Identifier("bottomRight"),
                    new Identifier("stroke"),
                    new Identifier("colour"),
                    new Identifier("strokeColour"))),
                this));

            fTable.Bind("Text", new Function(GasType.Text, new List<Variable>()
            {
                new Variable("value", GasType.String),
                new Variable("position", GasType.Point),
                new Variable("font", GasType.String),
                new Variable("fontSize", GasType.Number),
                new Variable("colour", GasType.Colour)
            }, new Return(new Text(
                new Identifier("value"),
                new Identifier("position"),
                new Identifier("font"),
                new Identifier("fontSize"),
                new Identifier("colour")
            )), this));

            fTable.Bind("Line", new Function(GasType.Line, new List<Variable>()
            {
                new Variable("start", GasType.Point),
                new Variable("end", GasType.Point),
                new Variable("stroke", GasType.Number),
                new Variable("colour", GasType.Colour),
                new Variable("strokeColour", GasType.Colour)
            }, new Return(new Line(
                new Identifier("start"),
                new Identifier("end"),
                new Identifier("stroke"),
                new Identifier("colour"),
                new Identifier("strokeColour")
            )), this));

            fTable.Bind("Square", new Function(GasType.Square, new List<Variable>()
            {
                new Variable("topLeft", GasType.Point),
                new Variable("size", GasType.Number),
                new Variable("stroke", GasType.Number),
                new Variable("colour", GasType.Colour),
                new Variable("strokeColour", GasType.Colour)
            }, new Return(new Square(
                new Identifier("topLeft"),
                new Identifier("size"),
                new Identifier("stroke"),
                new Identifier("colour"),
                new Identifier("strokeColour")
            )), this));

            fTable.Bind("Circle", new Function(GasType.Circle, new List<Variable>()
            {
                new Variable("centre", GasType.Point),
                new Variable("radius", GasType.Number),
                new Variable("stroke", GasType.Number),
                new Variable("colour", GasType.Colour),
                new Variable("strokeColour", GasType.Colour)
            }, new Return(new Circle(
                new Identifier("centre"),
                new Identifier("radius"),
                new Identifier("stroke"),
                new Identifier("colour"),
                new Identifier("strokeColour")
            )), this));
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
