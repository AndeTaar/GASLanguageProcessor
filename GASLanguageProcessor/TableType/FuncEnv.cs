using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using String = System.String;

namespace GASLanguageProcessor.TableType;

public class FuncEnv
{
    public Dictionary<string, Function> Functions { get; protected set; } = new();

    public FuncEnv? Parent { get; set; }

    public FuncEnv EnterScope()
    {
        return new FuncEnv(this);
    }

    public FuncEnv ExitScope()
    {
        return Parent ?? this;
    }

    public FuncEnv(FuncEnv parent)
    {
        this.Parent = parent;
    }

    public FuncEnv(Store store, VarEnv? parentEnv = null, FuncEnv? parent = null)
    {
        this.Parent = parent;

        if (parent != null)
        {
            return;
        }

        this.Bind("Color", new Function(new List<String>() {"red", "green", "blue", "alpha"},
            new Return(new Color(
                new Identifier("red"),
                new Identifier("green"),
                new Identifier("blue"),
                new Identifier("alpha"))),
            new VarEnv(parentEnv), this, store));

        this.Bind("Point", new Function(new List<String>() {"x", "y"},
            new Return(new Point(new Identifier("x"), new Identifier("y"))),
            new VarEnv(parentEnv), this, store));

        this.Bind("Line", new Function(new List<String>() {"start", "end", "stroke", "color"},
            new Return(new Line(new Identifier("start"), new Identifier("end"), new Identifier("stroke"),
                new Identifier("color"))), new VarEnv(parentEnv) , this, store));

        this.Bind("Circle", new Function(new List<String>() {"center", "radius", "stroke", "color", "strokeColor"},
            new Return(new Circle(new Identifier("center"), new Identifier("radius"), new Identifier("stroke"),
                new Identifier("color"), new Identifier("strokeColor"))), new VarEnv(parentEnv), this, store));

        this.Bind("Ellipse", new Function(new List<String>() {"center", "radiusX", "radiusY", "stroke", "color", "strokeColor"},
            new Return(new Ellipse(new Identifier("center"), new Identifier("radiusX"), new Identifier("radiusY"),
                new Identifier("stroke"), new Identifier("color"), new Identifier("strokeColor"))), new VarEnv(parentEnv), this, store));

        this.Bind("Triangle", new Function(new List<String>() {"point1", "point2", "color", "strokeColor", "stroke"},
            new Return(new Triangle(new Identifier("point1"), new Identifier("point2"), new Identifier("color"),
                new Identifier("strokeColor"), new Identifier("stroke"))), new VarEnv(parentEnv), this, store));

        this.Bind("Polygon", new Function(new List<String>() {"points", "stroke", "color", "strokeColor"},
            new Return(new Polygon(new Identifier("points"), new Identifier("stroke"), new Identifier("color"),
                new Identifier("strokeColor"))), new VarEnv(parentEnv), this, store));

        this.Bind("Rectangle", new Function(new List<String>() {"point1", "point2", "stroke", "color", "strokeColor", "rounding"},
            new Return(new Rectangle(new Identifier("point1"), new Identifier("point2"), new Identifier("stroke"),
                new Identifier("color"), new Identifier("strokeColor"), new Identifier("rounding"))), new VarEnv(parentEnv), this, store));

        this.Bind("Text", new Function(new List<String>() {"text", "position", "font", "fontSize", "fontWeight", "color"},
            new Return(new Text(new Identifier("text"), new Identifier("position"), new Identifier("font"),
                new Identifier("fontSize"), new Identifier("fontWeight"), new Identifier("color"))), new VarEnv(parentEnv), this, store));

        this.Bind("Square", new Function(new List<String>() {"point", "side", "stroke", "color", "strokeColor", "rounding"},
            new Return(new Square(new Identifier("point"), new Identifier("side"), new Identifier("stroke"),
                new Identifier("color"), new Identifier("strokeColor"), new Identifier("rounding"))), new VarEnv(parentEnv), this, store));

        this.Bind("SegLine", new Function(new List<String>() {"start", "end", "stroke", "color"},
            new Return(new SegLine(new Identifier("start"), new Identifier("end"), new Identifier("stroke"), new Identifier("color"))), new VarEnv(parentEnv), this, store));

        this.Bind("Arrow", new Function(new List<String>() {"start", "end", "stroke", "color"},
            new Return(new Arrow(new Identifier("start"), new Identifier("end"), new Identifier("stroke"), new Identifier("color"))), new VarEnv(parentEnv), this, store));

        this.Bind("AddToList", new Function(new List<String>() {"list", "element"},
            new Return(new AddToList(new Identifier("list"), new Identifier("element"))), new VarEnv(parentEnv), this, store));

        this.Bind("RemoveFromList", new Function(new List<String>() {"list", "index"},
            new Return(new RemoveFromList(new Identifier("list"), new Identifier("index"))), new VarEnv(parentEnv), this, store));

        this.Bind("GetFromList", new Function(new List<String>() {"list", "index"},
            new Return(new GetFromList(new Identifier("list"), new Identifier("index"))), new VarEnv(parentEnv), this, store));

        this.Bind("LengthOfList", new Function(new List<String>() {"list"},
            new Return(new LengthOfList(new Identifier("list"))), new VarEnv(parentEnv), this, store));


    }

    public bool Bind(string key, Function value)
    {
        if(Functions.ContainsKey(key))
        {
            return false;
        }
        Functions.Add(key, value);
        return true;
    }

    public Function? LookUp(string key)
    {
        if (Functions.ContainsKey(key))
        {
            return Functions[key];
        }
        return this.Parent?.LookUp(key);
    }
}
