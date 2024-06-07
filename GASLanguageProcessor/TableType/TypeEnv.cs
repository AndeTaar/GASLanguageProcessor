using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class TypeEnv
{
    public TypeEnv(TypeEnv? parent = null)
    {
        TypeEnvParent = parent;

        if (parent != null) return;

        RecTypeBind("Color", new Dictionary<string, GasType>
        {
            { "red", GasType.Num },
            { "green", GasType.Num },
            { "blue", GasType.Num },
            { "alpha", GasType.Num }
        }, GasType.Color);
        RecTypeBind("Point", new Dictionary<string, GasType>
        {
            { "x", GasType.Num },
            { "y", GasType.Num }
        }, GasType.Point);
        RecTypeBind("Rectangle", new Dictionary<string, GasType>
        {
            { "topLeft", GasType.Point },
            { "bottomRight", GasType.Point },
            { "stroke", GasType.Num },
            { "color", GasType.Color },
            { "strokeColor", GasType.Color },
            { "rounding", GasType.Num }
        }, GasType.Rectangle);
        RecTypeBind("Circle", new Dictionary<string, GasType>
        {
            { "center", GasType.Point },
            { "radius", GasType.Num },
            { "stroke", GasType.Num },
            { "color", GasType.Color },
            { "strokeColor", GasType.Color }
        }, GasType.Circle);
        RecTypeBind("Ellipse", new Dictionary<string, GasType>
        {
            { "center", GasType.Point },
            { "radiusX", GasType.Num },
            { "radiusY", GasType.Num },
            { "stroke", GasType.Num },
            { "color", GasType.Color },
            { "strokeColor", GasType.Color }
        }, GasType.Ellipse);
        RecTypeBind("Triangle", new Dictionary<string, GasType>
        {
            { "point1", GasType.Point },
            { "point2", GasType.Point },
            { "point3", GasType.Point },
            { "stroke", GasType.Num },
            { "color", GasType.Color },
            { "strokeColor", GasType.Color }
        }, GasType.Triangle);
        RecTypeBind("Polygon", new Dictionary<string, GasType>
        {
            { "points", GasType.Any },
            { "stroke", GasType.Num },
            { "color", GasType.Color },
            { "strokeColor", GasType.Color }
        }, GasType.Polygon);
        RecTypeBind("Line", new Dictionary<string, GasType>
        {
            { "startX", GasType.Num },
            { "startY", GasType.Num },
            { "endX", GasType.Num },
            { "color", GasType.Color }
        }, GasType.Line);
        RecTypeBind("SegLine", new Dictionary<string, GasType>
        {
            { "start", GasType.Point },
            { "end", GasType.Point },
            { "stroke", GasType.Num },
            { "color", GasType.Color }
        }, GasType.SegLine);
        RecTypeBind("Arrow", new Dictionary<string, GasType>
        {
            { "start", GasType.Point },
            { "end", GasType.Point },
            { "stroke", GasType.Num },
            { "color", GasType.Color },
            { "strokeColor", GasType.Color }
        }, GasType.Arrow);
        RecTypeBind("Square", new Dictionary<string, GasType>
        {
            { "topLeft", GasType.Point },
            { "side", GasType.Num },
            { "stroke", GasType.Num },
            { "color", GasType.Color },
            { "strokeColor", GasType.Color },
            { "rounding", GasType.Num }
        }, GasType.Square);
        RecTypeBind("Text", new Dictionary<string, GasType>
        {
            { "content", GasType.String },
            { "point", GasType.Point },
            { "font", GasType.String },
            { "size", GasType.Num },
            { "weight", GasType.Num },
            { "color", GasType.Color }
        }, GasType.Text);


        FBind("AddToList", new List<GasType> { GasType.Any, GasType.Any }, GasType.Any);
        FBind("RemoveFromList", new List<GasType> { GasType.Num, GasType.Any }, GasType.Void);
        FBind("GetFromList", new List<GasType> { GasType.Num, GasType.Any }, GasType.Num);
        FBind("LengthOfList", new List<GasType> { GasType.Any }, GasType.Num);
    }

    public Dictionary<string, GasType> VTypes { get; set; } = new();

    public Dictionary<string, (List<GasType>, GasType)> FTypes { get; set; } = new();

    public Dictionary<string, (List<GasType>, GasType)> ConstructorTypes { get; set; } = new();

    // string is the key to the constructor types
    public Dictionary<string, (Dictionary<string, GasType>, GasType)> RecordTypes { get; set; } = new();

    // string is the key to the record types
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

    public bool CBind(string key, List<GasType> parameters, GasType returnType)
    {
        if (ConstructorTypes.ContainsKey(key)) return false;
        ConstructorTypes.Add(key, (parameters, returnType));
        return true;
    }

    public bool RecBind(string key, string typeKey, TypeEnv env)
    {
        if (Records.ContainsKey(key)) return false;
        Records.Add(key, (typeKey, env));
        return true;
    }

    public bool RecTypeBind(string key, Dictionary<string, GasType> value, GasType returnType)
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

    public (Dictionary<string, GasType> fields, GasType recType)? RecTypeLookUp(string key)
    {
        if (RecordTypes.ContainsKey(key)) return RecordTypes[key];

        return TypeEnvParent?.RecTypeLookUp(key);
    }

    public ((Dictionary<string, GasType> fields, GasType type)?, TypeEnv recEnv)? RecLookUp(string key)
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

    public (List<GasType>, GasType)? CLookUp(string key)
    {
        if (ConstructorTypes.ContainsKey(key)) return ConstructorTypes[key];

        return TypeEnvParent?.CLookUp(key);
    }
}
