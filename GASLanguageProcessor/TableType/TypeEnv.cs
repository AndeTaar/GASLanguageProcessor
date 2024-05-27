using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class TypeEnv
{
    public Dictionary<string, GasType> VTypes { get; set; } = new();

    public Dictionary<string, (List<GasType>, GasType)> FTypes { get; set; } = new();



    public TypeEnv? TypeEnvParent { get; set; }


    public TypeEnv(TypeEnv? parent = null)
    {
        this.TypeEnvParent = parent;

        if (parent != null)
        {
            return;
        }

        this.FBind("Color", new List<GasType>() { GasType.Num, GasType.Num, GasType.Num, GasType.Num } ,GasType.Color);
        this.FBind("Point", new List<GasType>() { GasType.Num, GasType.Num }, GasType.Point);
        this.FBind("Rectangle", new List<GasType>() { GasType.Point, GasType.Point, GasType.Num, GasType.Color, GasType.Color, GasType.Num }, GasType.Rectangle);
        this.FBind("Circle", new List<GasType>() { GasType.Point, GasType.Num, GasType.Color, GasType.Color, GasType.Num }, GasType.Circle);
        this.FBind("Ellipse", new List<GasType>() { GasType.Point, GasType.Num, GasType.Num, GasType.Color, GasType.Color, GasType.Num }, GasType.Ellipse);
        this.FBind("Triangle", new List<GasType>() { GasType.Point, GasType.Point, GasType.Point, GasType.Color, GasType.Color, GasType.Num }, GasType.Triangle);
        this.FBind("Polygon", new List<GasType>() { GasType.List, GasType.Color, GasType.Color, GasType.Num }, GasType.Polygon);
        this.FBind("Line", new List<GasType>() { GasType.Point, GasType.Point, GasType.Num, GasType.Color }, GasType.Line);
        this.FBind("SegLine", new List<GasType>() { GasType.Point, GasType.Point, GasType.Num, GasType.Color }, GasType.SegLine);
        this.FBind("Arrow", new List<GasType>() { GasType.Point, GasType.Point, GasType.Num, GasType.Color, GasType.Color }, GasType.Arrow);
        this.FBind("Square", new List<GasType>() { GasType.Point, GasType.Num, GasType.Color, GasType.Color, GasType.Num }, GasType.Square);
        this.FBind("Group", new List<GasType>() { GasType.List }, GasType.Group);
        this.FBind("Canvas", new List<GasType>() { GasType.Num, GasType.Num }, GasType.Canvas);
        this.FBind("Text", new List<GasType>() { GasType.String, GasType.Point, GasType.String, GasType.Num, GasType.Num, GasType.Color }, GasType.Text);

        this.FBind("AddToList", new List<GasType>() { GasType.List, GasType.Any }, GasType.List);
        this.FBind("RemoveFromList", new List<GasType>() { GasType.List, GasType.Num }, GasType.List);
        this.FBind("GetFromList", new List<GasType>() { GasType.List, GasType.Num }, GasType.Any);
        this.FBind("LengthOfList", new List<GasType>() { GasType.List }, GasType.Num);
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

    public GasType? VLookUp(string key)
    {
        if (VTypes.ContainsKey(key))
        {
            return VTypes[key];
        }

        return this.TypeEnvParent?.VLookUp(key);
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