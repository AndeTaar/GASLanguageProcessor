﻿using System;
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
                    new Variable("red", this, GasType.Num),
                    new Variable("green", this, GasType.Num),
                    new Variable("blue", this, GasType.Num),
                    new Variable("alpha", this, GasType.Num)
                },
                new Return(new Color(
                    new Identifier("red"),
                    new Identifier("green"),
                    new Identifier("blue"),
                    new Identifier("alpha"))),
                new Scope(this, null)));

            fTable.Bind("Point", new Function(GasType.Point, new List<Variable>()
                {
                    new Variable("x", this, GasType.Num),
                    new Variable("y", this, GasType.Num)
                },
                new Return(new Point(
                    new Identifier("x"),
                    new Identifier("y"))),
                new Scope(this, null)));

            fTable.Bind("Rectangle", new Function(GasType.Rectangle, new List<Variable>()
                {
                    new Variable("topLeft", this, GasType.Point),
                    new Variable("bottomRight", this, GasType.Point),
                    new Variable("stroke", this, GasType.Num),
                    new Variable("colors", this, GasType.Color),
                    new Variable("strokeColors", this, GasType.Color),
                    new Variable("cornerRounding", this, GasType.Num)
                },
                new Return(new Rectangle(
                    new Identifier("topLeft"),
                    new Identifier("bottomRight"),
                    new Identifier("stroke"),
                    new Identifier("colors"),
                    new Identifier("strokeColors"),
                    new Identifier("cornerRounding"))),
                new Scope(this, null)));

            fTable.Bind("Text", new Function(GasType.Text, new List<Variable>()
            {
                new Variable("value", this, GasType.String),
                new Variable("position", this, GasType.Point),
                new Variable("font", this, GasType.String),
                new Variable("fontSize", this, GasType.Num),
                new Variable("fontWeight", this, GasType.Num),
                new Variable("colors", this, GasType.Color)
            }, new Return(new Text(
                new Identifier("value"),
                new Identifier("position"),
                new Identifier("font"),
                new Identifier("fontSize"),
                new Identifier("fontWeight"),
                new Identifier("colors")
            )), new Scope(this, null)));

            fTable.Bind("Line", new Function(GasType.Line, new List<Variable>()
            {
                new Variable("intercept", this, GasType.Num),
                new Variable("gradient", this, GasType.Num),
                new Variable("stroke", this, GasType.Num),
                new Variable("colors", this, GasType.Color)
            }, new Return(new Line(
                new Identifier("intercept"),
                new Identifier("gradient"),
                new Identifier("stroke"),
                new Identifier("colors")
            )), new Scope(this, null)));

            fTable.Bind("SegLine", new Function(GasType.SegLine, new List<Variable>()
            {
                new Variable("start", this, GasType.Point),
                new Variable("end", this, GasType.Point),
                new Variable("stroke", this, GasType.Num),
                new Variable("colors", this, GasType.Color)
            }, new Return(new SegLine(
                new Identifier("start"),
                new Identifier("end"),
                new Identifier("stroke"),
                new Identifier("colors")
            )), new Scope(this, null)));

            fTable.Bind("Arrow", new Function(GasType.Arrow, new List<Variable>()
            {
                new Variable("start", this, GasType.Point),
                new Variable("end", this, GasType.Point),
                new Variable("stroke", this, GasType.Num),
                new Variable("colors", this, GasType.Color)
            }, new Return(new Arrow(
                new Identifier("start"),
                new Identifier("end"),
                new Identifier("stroke"),
                new Identifier("colors")
            )), new Scope(this, null)));

            fTable.Bind("Square", new Function(GasType.Square, new List<Variable>()
            {
                new Variable("topLeft", this, GasType.Point),
                new Variable("length", this, GasType.Num),
                new Variable("stroke", this, GasType.Num),
                new Variable("colors", this, GasType.Color),
                new Variable("strokeColors", this, GasType.Color),
                new Variable("cornerRounding", this, GasType.Num)
            }, new Return(new Square(
                new Identifier("topLeft"),
                new Identifier("length"),
                new Identifier("stroke"),
                new Identifier("colors"),
                new Identifier("strokeColors"),
                new Identifier("cornerRounding")
            )), new Scope(this, null)));

            fTable.Bind("Circle", new Function(GasType.Circle, new List<Variable>()
            {
                new Variable("center", this, GasType.Point),
                new Variable("radius", this, GasType.Num),
                new Variable("stroke", this, GasType.Num),
                new Variable("colors", this, GasType.Color),
                new Variable("strokeColors", this, GasType.Color)
            }, new Return(new Circle(
                new Identifier("center"),
                new Identifier("radius"),
                new Identifier("stroke"),
                new Identifier("colors"),
                new Identifier("strokeColors")
            )), new Scope(this, null)));

            fTable.Bind("Ellipse", new Function(GasType.Ellipse, new List<Variable>()
            {
                new Variable("center", this, GasType.Point),
                new Variable("xRadius", this, GasType.Num),
                new Variable("yRadius", this, GasType.Num),
                new Variable("stroke", this, GasType.Num),
                new Variable("colors", this, GasType.Color),
                new Variable("strokeColors", this, GasType.Color)
            }, new Return(new Ellipse(
                new Identifier("center"),
                new Identifier("xRadius"),
                new Identifier("yRadius"),
                new Identifier("stroke"),
                new Identifier("colors"),
                new Identifier("strokeColors")
            )), new Scope(this, null)));

            fTable.Bind("AddToList", new Function(GasType.Void, new List<Variable>()
            {
                new Variable("value", this, GasType.Any),
                new Variable("list", this, GasType.Any)
            }, new Return(new AddToList(
                new Identifier("value"),
                new Identifier("list")
                )), new Scope(this, null)));

            fTable.Bind("RemoveFromList", new Function(GasType.Void, new List<Variable>()
            {
                new Variable("index", this, GasType.Num),
                new Variable("list", this, GasType.Any)
            }, new Return(new RemoveFromList(
                new Identifier("index"),
                new Identifier("list")
            )), new Scope(this, null)));

            fTable.Bind("GetFromList", new Function(GasType.Any, new List<Variable>()
            {
                new Variable("index", this, GasType.Num),
                new Variable("list", this, GasType.Any)
            }, new Return(new GetFromList(
                new Identifier("index"),
                new Identifier("list")
            )), new Scope(this, null)));

            fTable.Bind("LengthOfList", new Function(GasType.Num, new List<Variable>()
            {
                new Variable("list", this, GasType.Any)
            }, new Return(new LengthOfList(
                new Identifier("list")
            )), new Scope(this, null)));

            fTable.Bind("Polygon", new Function(GasType.Polygon, new List<Variable>()
            {
                new Variable("points", this, GasType.Any),
                new Variable("stroke", this, GasType.Num),
                new Variable("colors", this, GasType.Color),
                new Variable("strokeColors", this, GasType.Color)
            }, new Return(new Polygon(
                new Identifier("points"),
                new Identifier("stroke"),
                new Identifier("colors"),
                new Identifier("strokeColors")
            )), new Scope(this, null)));

            fTable.Bind("LinearGradient", new Function(GasType.Color, new List<Variable>()
            {
                new Variable("rotation", this, GasType.Num),
                new Variable("alpha", this, GasType.Num),
                new Variable("percentages", this, GasType.Any),
                new Variable("colors", this, GasType.Any)
            }, new Return(new LinearGradient(
                new Identifier("rotation"),
                new Identifier("alpha"),
                new Identifier("percentages"),
                new Identifier("colors")
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

    public Variable? LookupAttribute(Identifier identifier, Scope scope, Scope globalScope, List<string> errors)
    {
        if (identifier.ChildAttribute == null)
        {
            var var = scope.vTable.LookUp(identifier.Name);
            if (var == null)
            {
                return null;
            }
            return var;
        }
        var variable = scope?.vTable.LookUp(identifier.Name);
        if (variable == null)
        {
            errors.Add("Line: " + identifier.LineNum + " variable name: " + identifier.Name + " not found");
            return null;
        }

        return LookupAttribute(identifier.ChildAttribute, variable.FormalValue.Scope ?? globalScope, globalScope, errors);
    }

    public (Identifier, Function?) LookupMethod(Identifier identifier, Scope localScope, Scope globalScope, List<string> errors)
    {
        if (identifier.ChildAttribute == null)
        {
            var function = localScope?.fTable.LookUp(identifier.Name);
            if (function == null)
            {
                return (identifier, null);
            }
            return (identifier, function);
        }
        var variable = globalScope.vTable.LookUp(identifier.Name);
        if (variable == null)
        {
            return (identifier, null);
        }

        return LookupMethod(identifier.ChildAttribute, variable.Scope ?? globalScope, globalScope, errors);
    }
}
