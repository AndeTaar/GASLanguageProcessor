namespace GASLanguageProcessor.FinalTypes;

public class FinalArrow
{
    public FinalPoint Start { get; set; }
    public FinalPoint End { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColor StrokeColor { get; set; }
    public FinalTriangle ArrowHead { get; set; }
    
    public FinalArrow(FinalPoint start, FinalPoint end, float stroke, FinalColor strokeColor)
    {
        Start = start;
        End = end;
        Stroke = new FinalNum(stroke);
        StrokeColor = strokeColor;
        ArrowHead = GetArrowHead();
    }
    
    private FinalTriangle GetArrowHead()
    {
        
        //var distance = MathF.Sqrt(MathF.Pow(End.X.Value - Start.X.Value, 2) + MathF.Pow(End.Y.Value - Start.Y.Value, 2));
        var basePointX = Start.X.Value + (End.X.Value - Start.X.Value) * 0.9f;
        var basePointY = Start.Y.Value + (End.Y.Value - Start.Y.Value) * 0.9f;
        var basePoint = new FinalPoint(basePointX, basePointY);
        
        return new FinalTriangle(new FinalPoint(End.X.Value, End.Y.Value), basePoint, Stroke.Value, StrokeColor, StrokeColor);
        
        /*var x = End.X.Value - Start.X.Value;
        var y = End.Y.Value - Start.Y.Value;
        var angle = MathF.Atan2(y, x);
        var length = MathF.Sqrt(x * x + y * y);
        var peak = new FinalPoint(End.X.Value, End.Y.Value);
        var basePoint = new FinalPoint(End.X.Value - MathF.Cos(angle) * length / 10, End.Y.Value - MathF.Sin(angle) * length / 10);
        return new FinalTriangle(peak, basePoint, Stroke.Value, StrokeColor, StrokeColor);*/
    }
}