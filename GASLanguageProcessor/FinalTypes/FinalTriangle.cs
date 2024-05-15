using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalTriangle
{
    public FinalPoint TrianglePeak { get; set; }
    public List<FinalPoint> Points { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColor Color { get; set; }
    public FinalColor StrokeColor { get; set; }

    public override string ToString()
    {
        return TrianglePeak.ToString() + " " + new FinalPoint(Points[0].X.Value, Points[0].Y.Value).ToString() + " " + new FinalPoint(Points[1].X.Value, Points[1].Y.Value).ToString();
    }

    public FinalTriangle(FinalPoint trianglePeak, List<FinalPoint> points, float stroke, FinalColor color, FinalColor strokeColor)
    {
        TrianglePeak = trianglePeak;
        Points = points;
        Stroke = new FinalNum(stroke);
        Color = color;
        StrokeColor = strokeColor;
    }

}