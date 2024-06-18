using GASLanguageProcessor.AST.Types;
using GASLanguageProcessor.AST.Types.RecordType;
using GASLanguageProcessor.AST.Types.VariableType;

namespace GASLanguageProcessor.TableType;

public class TypeEnv
{
    public TypeEnv(TypeEnv? parent = null)
    {
        TypeEnvParent = parent;

        if (parent != null) return;

        RecTypeBind("Canvas", new Dictionary<string, GasType>
        {
            { "width", new VariableType(VariableTypes.Num) },
            { "height", new VariableType(VariableTypes.Num) },
            { "backgroundColor", new RecordType(GasRecordTypes.Color) }
        },GasRecordTypes.Canvas);
        RecTypeBind("Color", new Dictionary<string, GasType>
        {
            { "red", new VariableType(VariableTypes.Num) },
            { "green", new VariableType(VariableTypes.Num) },
            { "blue", new VariableType(VariableTypes.Num) },
            { "alpha", new VariableType(VariableTypes.Num) }
        },GasRecordTypes.Color);
        RecTypeBind("LinearGradient", new Dictionary<string, GasType>
        {
            { "colors", new ArrayType( new RecordType(GasRecordTypes.Color) ) },
            { "stops", new ArrayType( new VariableType(VariableTypes.Num) ) },
            { "rotation", new VariableType(VariableTypes.Num)},
            { "alpha", new VariableType(VariableTypes.Num) }
        },GasRecordTypes.Color);
        RecTypeBind("Point", new Dictionary<string, GasType>
        {
            { "x", new VariableType(VariableTypes.Num) },
            { "y", new VariableType(VariableTypes.Num) }
        },GasRecordTypes.Point);
        RecTypeBind("Rectangle", new Dictionary<string, GasType>
        {
            { "topLeft", new RecordType(GasRecordTypes.Point) },
            { "bottomRight", new RecordType(GasRecordTypes.Point) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(GasRecordTypes.Color) },
            { "strokeColor", new RecordType(GasRecordTypes.Color) },
            { "rounding", new VariableType(VariableTypes.Num) }
        },GasRecordTypes.Rectangle);
        RecTypeBind("Circle", new Dictionary<string, GasType>
        {
            { "center", new RecordType(GasRecordTypes.Point) },
            { "radius", new VariableType(VariableTypes.Num) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(GasRecordTypes.Color) },
            { "strokeColor", new RecordType(GasRecordTypes.Color) }
        },GasRecordTypes.Circle);
        RecTypeBind("Ellipse", new Dictionary<string, GasType>
        {
            { "center", new RecordType(GasRecordTypes.Point) },
            { "radiusX", new VariableType(VariableTypes.Num) },
            { "radiusY", new VariableType(VariableTypes.Num) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(GasRecordTypes.Color) },
            { "strokeColor", new RecordType(GasRecordTypes.Color) }
        },GasRecordTypes.Ellipse);
        RecTypeBind("Triangle", new Dictionary<string, GasType>
        {
            { "point1", new RecordType(GasRecordTypes.Point) },
            { "point2", new RecordType(GasRecordTypes.Point) },
            { "point3", new RecordType(GasRecordTypes.Point) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(GasRecordTypes.Color) },
            { "strokeColor", new RecordType(GasRecordTypes.Color) }
        },GasRecordTypes.Triangle);
        RecTypeBind("Polygon", new Dictionary<string, GasType>
        {
            { "points", new ArrayType(new RecordType(GasRecordTypes.Point))},
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(GasRecordTypes.Color) },
            { "strokeColor", new RecordType(GasRecordTypes.Color) }
        },GasRecordTypes.Polygon);
        RecTypeBind("Line", new Dictionary<string, GasType>
        {
            { "startX", new VariableType(VariableTypes.Num) },
            { "startY", new VariableType(VariableTypes.Num) },
            { "endX", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(GasRecordTypes.Color) }
        },GasRecordTypes.Line);
        RecTypeBind("SegLine", new Dictionary<string, GasType>
        {
            { "start", new RecordType(GasRecordTypes.Point) },
            { "end", new RecordType(GasRecordTypes.Point) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(GasRecordTypes.Color) }
        },GasRecordTypes.SegLine);
        RecTypeBind("Arrow", new Dictionary<string, GasType>
        {
            { "start", new RecordType(GasRecordTypes.Point) },
            { "end", new RecordType(GasRecordTypes.Point) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(GasRecordTypes.Color) },
            { "strokeColor", new RecordType(GasRecordTypes.Color) }
        },GasRecordTypes.Arrow);
        RecTypeBind("Square", new Dictionary<string, GasType>
        {
            { "topLeft", new RecordType(GasRecordTypes.Point) },
            { "side", new VariableType(VariableTypes.Num) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(GasRecordTypes.Color) },
            { "strokeColor", new RecordType(GasRecordTypes.Color) },
            { "rounding", new VariableType(VariableTypes.Num) }
        },GasRecordTypes.Square);
        RecTypeBind("Text", new Dictionary<string, GasType>
        {
            { "content", new VariableType(VariableTypes.String) },
            { "point", new RecordType(GasRecordTypes.Point) },
            { "font", new VariableType(VariableTypes.String) },
            { "size", new VariableType(VariableTypes.Num) },
            { "weight", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(GasRecordTypes.Color) }
        },GasRecordTypes.Text);
    }

    public Dictionary<string, VariableTypes> VTypes { get; set; } = new();

    public Dictionary<string, GroupType> GTypes { get; set; } = new();

    public Dictionary<string, ArrayType> ATypes { get; set; } = new();

    public Dictionary<string, (List<GasType>, GasType)> FTypes { get; set; } = new();

    public Dictionary<string, (Dictionary<string, GasType>, GasRecordTypes)> RecordTypes { get; set; } = new();

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

    public bool VBind(string key, VariableTypes value)
    {
        if (VTypes.ContainsKey(key)) return false;
        VTypes.Add(key, value);
        return true;
    }

    public bool GBind(string key, GroupType value)
    {
        if (GTypes.ContainsKey(key)) return false;
        GTypes.Add(key, value);
        return true;
    }

    public bool ABind(string key, ArrayType value)
    {
        if (ATypes.ContainsKey(key)) return false;
        ATypes.Add(key, value);
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

    public bool RecTypeBind(string key, Dictionary<string, GasType> value, GasRecordTypes returnType)
    {
        if (RecordTypes.ContainsKey(key)) return false;
        RecordTypes.Add(key, (value, returnType));
        return true;
    }

    public VariableTypes? VLookUp(string key)
    {
        if (VTypes.ContainsKey(key)) return VTypes[key];

        return TypeEnvParent?.VLookUp(key);
    }

    public ArrayType? ALookUp(string key)
    {
        if (ATypes.ContainsKey(key))
        {
            return ATypes[key];
        }

        return TypeEnvParent?.ALookUp(key);
    }

    public GroupType? GLookUp(string key)
    {
        if (GTypes.ContainsKey(key))
        {
            return GTypes[key];
        }

        return TypeEnvParent?.GLookUp(key);
    }

    public (Dictionary<string, GasType>, GasRecordTypes)? RecTypeLookUp(string key)
    {
        if (RecordTypes.ContainsKey(key)) return RecordTypes[key];

        return TypeEnvParent?.RecTypeLookUp(key);
    }

    public ((Dictionary<string, GasType>, GasRecordTypes)?, TypeEnv, string)? RecLookUp(string key)
    {
        if (Records.ContainsKey(key))
        {
            var record = Records[key];
            var recordType = RecTypeLookUp(record.Item1);
            return (recordType, record.Item2, key);
        }

        return TypeEnvParent?.RecLookUp(key);
    }

    public (List<GasType>, GasType)? FLookUp(string key)
    {
        if (FTypes.ContainsKey(key)) return FTypes[key];

        return TypeEnvParent?.FLookUp(key);
    }
}
