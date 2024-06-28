using CARLLanguageProcessor.AST.Types;
using CARLLanguageProcessor.AST.Types.RecordType;
using CARLLanguageProcessor.AST.Types.VariableType;

namespace CARLLanguageProcessor.TableType;

public class TypeEnv
{
    public TypeEnv(TypeEnv? parent = null)
    {
        TypeEnvParent = parent;

        if (parent != null) return;

        RecTypeBind("Canvas", new Dictionary<string, CARLType>
        {
            { "width", new VariableType(VariableTypes.Num) },
            { "height", new VariableType(VariableTypes.Num) },
            { "backgroundColor", new RecordType(CARLRecordTypes.Color) }
        },CARLRecordTypes.Canvas);
        RecTypeBind("Color", new Dictionary<string, CARLType>
        {
            { "red", new VariableType(VariableTypes.Num) },
            { "green", new VariableType(VariableTypes.Num) },
            { "blue", new VariableType(VariableTypes.Num) },
            { "alpha", new VariableType(VariableTypes.Num) }
        },CARLRecordTypes.Color);
        RecTypeBind("LinearGradient", new Dictionary<string, CARLType>
        {
            { "colors", new ArrayType( new RecordType(CARLRecordTypes.Color) ) },
            { "stops", new ArrayType( new VariableType(VariableTypes.Num) ) },
            { "rotation", new VariableType(VariableTypes.Num)},
            { "alpha", new VariableType(VariableTypes.Num) }
        },CARLRecordTypes.Color);
        RecTypeBind("Point", new Dictionary<string, CARLType>
        {
            { "x", new VariableType(VariableTypes.Num) },
            { "y", new VariableType(VariableTypes.Num) }
        },CARLRecordTypes.Point);
        RecTypeBind("Rectangle", new Dictionary<string, CARLType>
        {
            { "topLeft", new RecordType(CARLRecordTypes.Point) },
            { "bottomRight", new RecordType(CARLRecordTypes.Point) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(CARLRecordTypes.Color) },
            { "strokeColor", new RecordType(CARLRecordTypes.Color) },
            { "rounding", new VariableType(VariableTypes.Num) }
        },CARLRecordTypes.Rectangle);
        RecTypeBind("Circle", new Dictionary<string, CARLType>
        {
            { "center", new RecordType(CARLRecordTypes.Point) },
            { "radius", new VariableType(VariableTypes.Num) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(CARLRecordTypes.Color) },
            { "strokeColor", new RecordType(CARLRecordTypes.Color) }
        },CARLRecordTypes.Circle);
        RecTypeBind("Ellipse", new Dictionary<string, CARLType>
        {
            { "center", new RecordType(CARLRecordTypes.Point) },
            { "radiusX", new VariableType(VariableTypes.Num) },
            { "radiusY", new VariableType(VariableTypes.Num) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(CARLRecordTypes.Color) },
            { "strokeColor", new RecordType(CARLRecordTypes.Color) }
        },CARLRecordTypes.Ellipse);
        RecTypeBind("Triangle", new Dictionary<string, CARLType>
        {
            { "point1", new RecordType(CARLRecordTypes.Point) },
            { "point2", new RecordType(CARLRecordTypes.Point) },
            { "point3", new RecordType(CARLRecordTypes.Point) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(CARLRecordTypes.Color) },
            { "strokeColor", new RecordType(CARLRecordTypes.Color) }
        },CARLRecordTypes.Triangle);
        RecTypeBind("Polygon", new Dictionary<string, CARLType>
        {
            { "points", new ArrayType(new RecordType(CARLRecordTypes.Point))},
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(CARLRecordTypes.Color) },
            { "strokeColor", new RecordType(CARLRecordTypes.Color) }
        },CARLRecordTypes.Polygon);
        RecTypeBind("Line", new Dictionary<string, CARLType>
        {
            { "startX", new VariableType(VariableTypes.Num) },
            { "startY", new VariableType(VariableTypes.Num) },
            { "endX", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(CARLRecordTypes.Color) }
        },CARLRecordTypes.Line);
        RecTypeBind("SegLine", new Dictionary<string, CARLType>
        {
            { "start", new RecordType(CARLRecordTypes.Point) },
            { "end", new RecordType(CARLRecordTypes.Point) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(CARLRecordTypes.Color) }
        },CARLRecordTypes.SegLine);
        RecTypeBind("Arrow", new Dictionary<string, CARLType>
        {
            { "start", new RecordType(CARLRecordTypes.Point) },
            { "end", new RecordType(CARLRecordTypes.Point) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(CARLRecordTypes.Color) },
            { "strokeColor", new RecordType(CARLRecordTypes.Color) }
        },CARLRecordTypes.Arrow);
        RecTypeBind("Square", new Dictionary<string, CARLType>
        {
            { "topLeft", new RecordType(CARLRecordTypes.Point) },
            { "side", new VariableType(VariableTypes.Num) },
            { "stroke", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(CARLRecordTypes.Color) },
            { "strokeColor", new RecordType(CARLRecordTypes.Color) },
            { "rounding", new VariableType(VariableTypes.Num) }
        },CARLRecordTypes.Square);
        RecTypeBind("Text", new Dictionary<string, CARLType>
        {
            { "content", new VariableType(VariableTypes.String) },
            { "point", new RecordType(CARLRecordTypes.Point) },
            { "font", new VariableType(VariableTypes.String) },
            { "size", new VariableType(VariableTypes.Num) },
            { "weight", new VariableType(VariableTypes.Num) },
            { "color", new RecordType(CARLRecordTypes.Color) }
        },CARLRecordTypes.Text);
    }

    public Dictionary<string, VariableTypes> VTypes { get; set; } = new();

    public Dictionary<string, GroupType> GTypes { get; set; } = new();

    public Dictionary<string, ArrayType> ATypes { get; set; } = new();

    public Dictionary<string, (List<CARLType>, CARLType)> FTypes { get; set; } = new();

    public Dictionary<string, (Dictionary<string, CARLType>, CARLRecordTypes)> RecordTypes { get; set; } = new();

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

    public bool FBind(string key, List<CARLType> parameters, CARLType returnType)
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

    public bool RecTypeBind(string key, Dictionary<string, CARLType> value, CARLRecordTypes returnType)
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

    public (Dictionary<string, CARLType>, CARLRecordTypes)? RecTypeLookUp(string key)
    {
        if (RecordTypes.ContainsKey(key)) return RecordTypes[key];

        return TypeEnvParent?.RecTypeLookUp(key);
    }

    public ((Dictionary<string, CARLType>, CARLRecordTypes)?, TypeEnv, string)? RecLookUp(string key)
    {
        if (Records.ContainsKey(key))
        {
            var record = Records[key];
            var recordType = RecTypeLookUp(record.Item1);
            return (recordType, record.Item2, key);
        }

        return TypeEnvParent?.RecLookUp(key);
    }

    public (List<CARLType>, CARLType)? FLookUp(string key)
    {
        if (FTypes.ContainsKey(key)) return FTypes[key];

        return TypeEnvParent?.FLookUp(key);
    }
}
