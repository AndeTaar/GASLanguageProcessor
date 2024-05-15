using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalTriangle
{
    public FinalPoint TrianglePeak { get; set; }
    public FinalPoint TriangleBase { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColor Color { get; set; }
    public FinalColor StrokeColor { get; set; }
    
    public FinalPolygon AsPolygon => ToPolygon();
    
    public FinalPolygon ToPolygon()
    {
        var points = new FinalList(new List<object>(), new Scope(null,null));
        points.Values.Add(TrianglePeak);
        var basePoints = FindBasePoints();
        points.Values.Add(basePoints[0]);
        points.Values.Add(basePoints[1]);
        return new FinalPolygon(points, Stroke.Value, Color, StrokeColor);
    }
    
    private FinalPoint[] FindBasePoints()
    {
        var basePoints = new FinalPoint[2];
        
        var baseLineGradient = -1 / ((TrianglePeak.Y.Value - TriangleBase.Y.Value) / (TrianglePeak.X.Value - TriangleBase.X.Value));
        
        var basePoint1X = TriangleBase.X.Value + 1;
        var basePoint1Y = TriangleBase.Y.Value + baseLineGradient;
        
        var basePoint1 = new FinalPoint(basePoint1X, basePoint1Y);
        
        var basePoint2X = TriangleBase.X.Value - 1;
        var basePoint2Y = TriangleBase.Y.Value - baseLineGradient;
        
        var basePoint2 = new FinalPoint(basePoint2X, basePoint2Y);
        
        basePoints[0] = basePoint1; basePoints[1] = basePoint2;
        
        return basePoints;
    }

    public FinalTriangle(FinalPoint trianglePeak, FinalPoint triangleBase, float stroke, FinalColor color, FinalColor strokeColor)
    {
        TrianglePeak = trianglePeak;
        TriangleBase = triangleBase;
        Stroke = new FinalNum(stroke);
        Color = color;
        StrokeColor = strokeColor;
    }
    
}