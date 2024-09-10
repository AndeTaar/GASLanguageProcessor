using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalPoint
{
    public FinalNum X { get; set; }
    public FinalNum Y { get; set; }

    public FinalPoint(float x, float y)
    {
        X = new FinalNum(x);
        Y = new FinalNum(y);
    }

    public FinalPoint(Struct s)
    {
        X = new FinalNum(s.Values["x"] as float? ?? 0);
        Y = new FinalNum(s.Values["y"] as float? ?? 0);
    }

    public FinalPoint(FinalPoint obj)
    {
        X = obj.X;
        Y = obj.Y;
    }

    public FinalPoint(object obj)
    {
        if (obj is FinalPoint p)
        {
            X = p.X;
            Y = p.Y;
        }

        if (obj is Struct s)
        {
            X = new FinalNum(s.Values["x"] as float? ?? 0);
            Y = new FinalNum(s.Values["y"] as float? ?? 0);
        }
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}
