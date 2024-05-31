﻿using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class TypeEnv
{
    public Dictionary<string, List<GasType>> VTypes { get; set; } = new();

    public Dictionary<string, Dictionary<string, GasType>> STypes { get; set; } = new();

    public Dictionary<string, (List<GasType>, GasType)> FTypes { get; set; } = new();

    public TypeEnv? TypeEnvParent { get; set; }


    public TypeEnv(TypeEnv? parent = null)
    {
        this.TypeEnvParent = parent;

        if (parent != null)
        {
            return;
        }

        this.SBind("Color", new Dictionary<string, GasType>() { { "red", GasType.Num }, { "green", GasType.Num }, { "blue", GasType.Num }, { "alpha", GasType.Num } });
        this.SBind("Point", new Dictionary<string, GasType>() { { "x", GasType.Num }, { "y", GasType.Num } });
        this.SBind("Rectangle", new Dictionary<string, GasType>() { { "topLeft", GasType.Any }, { "bottomRight", GasType.Any }, { "borderWidth", GasType.Num }, { "borderColor", GasType.Quadruple }, { "fillColor", GasType.Quadruple }, { "borderRadius", GasType.Num } });
        this.SBind("Circle", new Dictionary<string, GasType>() { { "center", GasType.Any }, { "radius", GasType.Num }, { "borderWidth", GasType.Num }, { "borderColor", GasType.Quadruple }, { "fillColor", GasType.Quadruple } });
        this.SBind("Ellipse", new Dictionary<string, GasType>() { { "center", GasType.Any }, { "radiusX", GasType.Num }, { "radiusY", GasType.Num }, { "borderWidth", GasType.Num }, { "borderColor", GasType.Quadruple }, { "fillColor", GasType.Quadruple } });
        this.SBind("Triangle", new Dictionary<string, GasType>() { { "point1", GasType.Any }, { "point2", GasType.Any }, { "point3", GasType.Any }, { "borderColor", GasType.Quadruple }, { "fillColor", GasType.Quadruple }, { "borderWidth", GasType.Num } });
        this.SBind("Polygon", new Dictionary<string, GasType>() { { "points", GasType.List }, { "borderWidth", GasType.Num }, { "borderColor", GasType.Quadruple }, { "fillColor", GasType.Quadruple } });
        this.SBind("Line", new Dictionary<string, GasType>() { { "x1", GasType.Num }, { "y1", GasType.Num }, { "x2", GasType.Num }, { "y2", GasType.Num }, { "color", GasType.Quadruple } });
        this.SBind("SegLine", new Dictionary<string, GasType>() { { "start", GasType.Any }, { "end", GasType.Any }, { "width", GasType.Num }, { "color", GasType.Quadruple } });
        this.SBind("Arrow", new Dictionary<string, GasType>() { { "start", GasType.Any }, { "end", GasType.Any }, { "width", GasType.Num }, { "color", GasType.Quadruple }, { "fillColor", GasType.Quadruple } });
        this.SBind("Square", new Dictionary<string, GasType>() { { "topLeft", GasType.Any }, { "width", GasType.Num }, { "height", GasType.Num }, { "borderColor", GasType.Quadruple }, { "fillColor", GasType.Quadruple }, { "borderWidth", GasType.Num } });
        this.SBind("Group", new Dictionary<string, GasType>() { { "shapes", GasType.List } });
        this.SBind("Text", new Dictionary<string, GasType>() { { "text", GasType.String }, { "position", GasType.Any }, { "font", GasType.String }, { "fontSize", GasType.Num }, { "fontStyle", GasType.Num }, { "color", GasType.Quadruple } });

        this.FBind("AddToList", new List<GasType>() { GasType.Any, GasType.Any }, GasType.Any);
        this.FBind("RemoveFromList", new List<GasType>() { GasType.Num, GasType.Any }, GasType.Void);
        this.FBind("GetFromList", new List<GasType>() { GasType.Num, GasType.Any }, GasType.Num);
        this.FBind("LengthOfList", new List<GasType>() { GasType.Any }, GasType.Num);

    }

    public TypeEnv EnterScope()
    {
        return new TypeEnv(this);
    }

    public TypeEnv ExitScope()
    {
        return this.TypeEnvParent ?? this;
    }

    public bool VBind(string key, List<GasType> type)
    {
        if(VTypes.ContainsKey(key))
        {
            return false;
        }
        VTypes.Add(key, type);
        return true;
    }

    public bool SBind(string key, Dictionary<string, GasType> types)
    {
        if(STypes.ContainsKey(key))
        {
            return false;
        }
        STypes.Add(key, types);
        return true;
    }

    public bool FBind(string key, List<GasType> parameters, GasType returnType)
    {
        if(FTypes.ContainsKey(key))
        {
            return false;
        }
        FTypes.Add(key, (parameters, returnType));
        return true;
    }

    public List<GasType>? VLookUp(string key)
    {
        if (VTypes.ContainsKey(key))
        {
            return VTypes[key];
        }

        return this.TypeEnvParent?.VLookUp(key);
    }

    public GasType? SLookUp(string key, string childKey)
    {
        if (VTypes.ContainsKey(key))
        {

        }
        if (STypes.ContainsKey(key))
        {
            return STypes[key][childKey];
        }

        return this.TypeEnvParent?.SLookUp(key, childKey) ?? GasType.Error;
    }

    public Dictionary<string, GasType>? SLookUpFieldTypes(string key)
    {
        if (STypes.ContainsKey(key))
        {
            return STypes[key];
        }

        return this.TypeEnvParent?.SLookUpFieldTypes(key);
    }

    public (List<GasType>, GasType)? FLookUp(string key)
    {
        if (FTypes.ContainsKey(key))
        {
            return FTypes[key];
        }

        return this.TypeEnvParent?.FLookUp(key);
    }

}
