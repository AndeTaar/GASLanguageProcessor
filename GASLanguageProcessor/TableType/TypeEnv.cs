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

        TypeEnv envT = this.EnterScope();
        envT.VBind("red", GasType.Num);
        envT.VBind("green", GasType.Num);
        envT.VBind("blue", GasType.Num);
        envT.VBind("alpha", GasType.Num);

        RecTypeBind("Color", envT);

        envT = this.EnterScope();
        envT.VBind("x", GasType.Num);
        envT.VBind("y", GasType.Num);

        RecTypeBind("Point", envT);

        envT = this.EnterScope();
        envT.VBind("bottomRight", GasType.Point);
        envT.VBind("topLeft", GasType.Point);
        envT.VBind("stroke", GasType.Num);
        envT.VBind("color", GasType.Color);
        envT.VBind("strokeColor", GasType.Color);
        envT.VBind("rounding", GasType.Num);

        RecTypeBind("Rectangle", envT);

        envT = this.EnterScope();
        envT.VBind("center", GasType.Point);
        envT.VBind("radius", GasType.Num);
        envT.VBind("stroke", GasType.Num);
        envT.VBind("color", GasType.Color);
        envT.VBind("strokeColor", GasType.Color);

        RecTypeBind("Circle", envT);

        envT = this.EnterScope();
        envT.VBind("text", GasType.String);
        envT.VBind("point", GasType.Point);
        envT.VBind("size", GasType.Num);
        envT.VBind("stroke", GasType.Num);
envT.VBind("color", GasType.Color);



        FBind("AddToList", new List<GasType> { GasType.Any, GasType.Any }, GasType.Any);
        FBind("RemoveFromList", new List<GasType> { GasType.Num, GasType.Any }, GasType.Void);
        FBind("GetFromList", new List<GasType> { GasType.Num, GasType.Any }, GasType.Num);
        FBind("LengthOfList", new List<GasType> { GasType.Any }, GasType.Num);
    }

    public Dictionary<string, GasType> VTypes { get; set; } = new();

    public Dictionary<string, (List<GasType>, GasType)> FTypes { get; set; } = new();

    public Dictionary<string, TypeEnv> RecordTypes { get; set; } = new();

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

    public bool RecTypeBind(string key, TypeEnv env)
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
