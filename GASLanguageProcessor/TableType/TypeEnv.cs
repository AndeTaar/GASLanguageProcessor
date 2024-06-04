using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class TypeEnv
{
    public Dictionary<string, GasType> VTypes { get; set; } = new();

    public Dictionary<string, (List<GasType>, GasType)> FTypes { get; set; } = new();

    public Dictionary<string, (Dictionary<string, GasType>, GasType)> RecordTypes { get; set; } = new();

    public Dictionary<string, (string, TypeEnv)> Records { get; set; } = new();

    public TypeEnv? TypeEnvParent { get; set; }


    public TypeEnv(TypeEnv? parent = null)
    {
        this.TypeEnvParent = parent;

        if (parent != null)
        {
            return;
        }

        this.RecTypeBind("Color", new Dictionary<string, GasType>()
        {
            { "red", GasType.Num },
            { "green", GasType.Num },
            { "blue", GasType.Num },
            { "alpha", GasType.Num }
        }, GasType.Color);
        this.RecTypeBind("Point", new Dictionary<string, GasType>()
        {
            { "x", GasType.Num },
            { "y", GasType.Num }
        }, GasType.Point);
        this.RecTypeBind("Rectangle", new Dictionary<string, GasType>()
        {
            { "topLeft", GasType.Point },
            { "bottomRight", GasType.Point },
            { "borderWidth", GasType.Num },
            { "borderColor", GasType.Color },
            { "fillColor", GasType.Color },
            { "rounding", GasType.Num }
        }, GasType.Rectangle);
        this.RecTypeBind("Circle", new Dictionary<string, GasType>()
        {
            { "center", GasType.Point },
            { "radius", GasType.Num },
            { "stroke", GasType.Num },
            { "color", GasType.Color },
            { "strokeColor", GasType.Color }
        }, GasType.Circle);
        this.RecTypeBind("Ellipse", new Dictionary<string, GasType>()
        {
            { "center", GasType.Point },
            { "radiusX", GasType.Num },
            { "radiusY", GasType.Num },
            { "borderWidth", GasType.Num },
            { "borderColor", GasType.Color },
            { "fillColor", GasType.Color }
        }, GasType.Ellipse);
        this.RecTypeBind("Triangle", new Dictionary<string, GasType>()
        {
            { "point1", GasType.Point },
            { "point2", GasType.Point },
            { "point3", GasType.Point },
            { "borderColor", GasType.Color },
            { "fillColor", GasType.Color },
            { "borderWidth", GasType.Num }
        }, GasType.Triangle);
        this.RecTypeBind("Polygon", new Dictionary<string, GasType>()
        {
            { "points", GasType.Any },
            { "stroke", GasType.Num },
            { "color", GasType.Color },
            { "strokeColor", GasType.Color }
        }, GasType.Polygon);
        this.RecTypeBind("Line", new Dictionary<string, GasType>()
        {
            { "startX", GasType.Num },
            { "startY", GasType.Num },
            { "endX", GasType.Num },
            { "color", GasType.Color }
        }, GasType.Line);
        this.RecTypeBind("SegLine", new Dictionary<string, GasType>()
        {
            { "startPoint", GasType.Point },
            { "endPoint", GasType.Point },
            { "width", GasType.Num },
            { "color", GasType.Color }
        }, GasType.SegLine);
        this.RecTypeBind("Arrow", new Dictionary<string, GasType>()
        {
            { "startPoint", GasType.Point },
            { "endPoint", GasType.Point },
            { "width", GasType.Num },
            { "borderColor", GasType.Color },
            { "fillColor", GasType.Color }
        }, GasType.Arrow);
        this.RecTypeBind("Square", new Dictionary<string, GasType>()
        {
            { "topLeft", GasType.Point },
            { "sideLength", GasType.Num },
            { "borderWidth", GasType.Num },
            { "borderColor", GasType.Color },
            { "fillColor", GasType.Color },
            { "rotation", GasType.Num }
        }, GasType.Square);
        this.RecTypeBind("Text", new Dictionary<string, GasType>()
        {
            { "content", GasType.String },
            { "position", GasType.Point },
            { "font", GasType.String },
            { "size", GasType.Num },
            { "weight", GasType.Num },
            { "color", GasType.Color }
        }, GasType.Text);


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

    public bool VBind(string key, GasType value)
    {
        if(VTypes.ContainsKey(key))
        {
            return false;
        }
        VTypes.Add(key, value);
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

    public bool RecBind(string key, string typeKey, TypeEnv env)
    {
        if(Records.ContainsKey(key))
        {
            return false;
        }
        Records.Add(key, (typeKey, env));
        return true;
    }

    public bool RecTypeBind(string key, Dictionary<string, GasType> value, GasType returnType)
    {
        if(RecordTypes.ContainsKey(key))
        {
            return false;
        }
        RecordTypes.Add(key, (value,returnType));
        return true;
    }

    public GasType? VLookUp(string key)
    {
        if (VTypes.ContainsKey(key))
        {
            return VTypes[key];
        }

        return this.TypeEnvParent?.VLookUp(key);
    }

    public (Dictionary<string, GasType>, GasType)? RecTypeLookUp(string key)
    {
        if (RecordTypes.ContainsKey(key))
        {
            return RecordTypes[key];
        }

        return this.TypeEnvParent?.RecTypeLookUp(key);
    }

    public ((Dictionary<string, GasType>, GasType)?, TypeEnv)? RecLookUp(string key)
    {
        if (Records.ContainsKey(key))
        {
            var record = Records[key];
            var recordType = this.RecTypeLookUp(record.Item1);
            return (recordType, record.Item2);
        }

        return this.TypeEnvParent?.RecLookUp(key);
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
