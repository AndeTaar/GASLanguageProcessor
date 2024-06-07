using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;

namespace GASLanguageProcessor.TableType;

public class TypeEnv
{
    public TypeEnv(TypeEnv? parent = null)
    {
        TypeEnvParent = parent;

        if (parent != null) return;

        RecTypeBind("Color", new Dictionary<string, (GasType type, Expression defaultVal)>
        {
            { "red", (GasType.Num, new Num("255")) },
            { "green", (GasType.Num, new Num("255")) },
            { "blue", (GasType.Num, new Num("255")) },
            { "alpha", (GasType.Num, new Num("1")) }
        }, GasType.Color);
        RecTypeBind("Point", new Dictionary<string, (GasType type, Expression defaultVal)>
        {
            { "x", (GasType.Num, new Num("0")) },
            { "y", (GasType.Num, new Num("0")) }
        }, GasType.Point);
        RecTypeBind("Rectangle", new Dictionary<string, (GasType type, Expression defaultVal)>
        {
            { "topLeft", (GasType.Point, null) },
            { "bottomRight", (GasType.Point, null) },
            { "stroke", (GasType.Num, new Num("255")) },
            { "color", (GasType.Color, null) },
            { "strokeColor", (GasType.Color, null) },
            { "rounding", (GasType.Num, new Num("255")) }
        }, GasType.Rectangle);
        RecTypeBind("Circle", new Dictionary<string, (GasType Point, Expression defaultVal)>
        {
            { "center", (GasType.Point, null)},
            { "radius", (GasType.Num, new Num("10")) },
            { "stroke", (GasType.Num, new Num("5")) },
            { "color",  (GasType.Color, null)},
            { "strokeColor", (GasType.Color, null) }
        }, GasType.Circle);
        RecTypeBind("Ellipse", new Dictionary<string, (GasType type, Expression defaultVal)>
        {
            { "center", (GasType.Point, null) },
            { "radiusX", (GasType.Num, new Num("255")) },
            { "radiusY", (GasType.Num, new Num("255")) },
            { "stroke", (GasType.Num, new Num("255")) },
            { "color", (GasType.Color, null) },
            { "strokeColor", (GasType.Color, null) }
        }, GasType.Ellipse);
        RecTypeBind("Triangle", new Dictionary<string, (GasType type, Expression defaultVal)>
        {
            { "point1", (GasType.Point, null) },
            { "point2", (GasType.Point, null) },
            { "point3", (GasType.Point, null) },
            { "stroke", (GasType.Num, new Num("255")) },
            { "color", (GasType.Color, null) },
            { "strokeColor", (GasType.Color, null) }
        }, GasType.Triangle);
        RecTypeBind("Polygon", new Dictionary<string, (GasType type, Expression defaultVal)>
        {
            { "points", (GasType.Any, null) },
            { "stroke", (GasType.Num, new Num("255")) },
            { "color", (GasType.Color, null) },
            { "strokeColor", (GasType.Color, null) }
        }, GasType.Polygon);
        RecTypeBind("Line", new Dictionary<string, (GasType type, Expression defaultVal)>
        {
            { "startX", (GasType.Num, new Num("255")) },
            { "startY", (GasType.Num, new Num("255")) },
            { "endX", (GasType.Num, new Num("255")) },
            { "color", (GasType.Color, null) }
        }, GasType.Line);
        RecTypeBind("SegLine", new Dictionary<string, (GasType type, Expression defaultVal)>
        {
            { "start", (GasType.Point, null) },
            { "end", (GasType.Point, null) },
            { "stroke", (GasType.Num, new Num("255")) },
            { "color", (GasType.Color, null) }
        }, GasType.SegLine);
        RecTypeBind("Arrow", new Dictionary<string, (GasType type, Expression defaultVal)>
        {
            { "start", (GasType.Point, null) },
            { "end", (GasType.Point, null) },
            { "stroke", (GasType.Num, new Num("255")) },
            { "color", (GasType.Color, null) },
            { "strokeColor", (GasType.Color, null) }
        }, GasType.Arrow);
        RecTypeBind("Square", new Dictionary<string, (GasType type, Expression defaultVal)>
        {
            { "topLeft", (GasType.Point, null) },
            { "side", (GasType.Num, new Num("255")) },
            { "stroke", (GasType.Num, new Num("255")) },
            { "color", (GasType.Color, null) },
            { "strokeColor", (GasType.Color, null) },
            { "rounding", (GasType.Num, new Num("255")) }
        }, GasType.Square);
        RecTypeBind("Text", new Dictionary<string, (GasType type, Expression defaultVal)>
        {
            { "content", (GasType.Color, null) },
            { "point", (GasType.Point, null) },
            { "font", (GasType.Color, null) },
            { "size", (GasType.Num, new Num("255")) },
            { "weight", (GasType.Num, new Num("255")) },
            { "color", (GasType.Color, null) }
        }, GasType.Text);


        FBind("AddToList", new List<GasType> { GasType.Any, GasType.Any }, GasType.Any);
        FBind("RemoveFromList", new List<GasType> { GasType.Num, GasType.Any }, GasType.Void);
        FBind("GetFromList", new List<GasType> { GasType.Num, GasType.Any }, GasType.Num);
        FBind("LengthOfList", new List<GasType> { GasType.Any }, GasType.Num);
    }

    public Dictionary<string, GasType> VTypes { get; set; } = new();

    public Dictionary<string, (List<GasType>, GasType)> FTypes { get; set; } = new();

    public Dictionary<string, (Dictionary<string, (GasType type, Expression defaultVal)>, GasType recType)> RecordTypes { get; set; } = new();

    public Dictionary<string, (string, TypeEnv)> Records { get; set; } = new();

    public TypeEnv? TypeEnvParent { get; set; }

    public TypeEnv EnterScope()
    {
        return new TypeEnv(this);
    }

    public TypeEnv ExitScope()
    {
        return TypeEnvParent ?? this;
    }

    public bool VBind(string key, GasType value)
    {
        if (VTypes.ContainsKey(key)) return false;
        VTypes.Add(key, value);
        return true;
    }

    public bool FBind(string key, List<GasType> parameters, GasType returnType)
    {
        if (FTypes.ContainsKey(key)) return false;
        FTypes.Add(key, (parameters, returnType));
        return true;
    }

    public bool RecBind(string key, string typeKey, TypeEnv env)
    {
        if (Records.ContainsKey(key)) return false;
        Records.Add(key, (typeKey, env));
        return true;
    }

    public bool RecTypeBind(string key, Dictionary<string, (GasType type, Expression defaultVal)> value, GasType returnType)
    {
        if (RecordTypes.ContainsKey(key)) return false;
        RecordTypes.Add(key, (value, returnType));
        return true;
    }

    public GasType? VLookUp(string key)
    {
        if (VTypes.ContainsKey(key)) return VTypes[key];

        return TypeEnvParent?.VLookUp(key);
    }

    public (Dictionary<string, (GasType type, Expression defaultVal)> fields, GasType recType)? RecTypeLookUp(string key)
    {
        if (RecordTypes.ContainsKey(key)) return RecordTypes[key];

        return TypeEnvParent?.RecTypeLookUp(key);
    }

    public ((Dictionary<string, (GasType type, Expression defaultVal)> fields, GasType recType)?, TypeEnv)? RecLookUp(string key)
    {
        if (Records.ContainsKey(key))
        {
            var record = Records[key];
            var recordType = RecTypeLookUp(record.Item1);
            return (recordType, record.Item2);
        }

        return TypeEnvParent?.RecLookUp(key);
    }

    public (List<GasType>, GasType)? FLookUp(string key)
    {
        if (FTypes.ContainsKey(key)) return FTypes[key];

        return TypeEnvParent?.FLookUp(key);
    }
}
