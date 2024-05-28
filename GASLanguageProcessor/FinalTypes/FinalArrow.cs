using System.Numerics;

namespace GASLanguageProcessor.FinalTypes;

public class FinalArrow
{
    public FinalPoint Start { get; set; }
    public FinalPoint End { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColors StrokeColors { get; set; }
    public FinalTriangle ArrowHead { get; set; }

    public FinalArrow(FinalPoint start, FinalPoint end, float stroke, FinalColors strokeColors)
    {
        Start = start;
        End = end;
        Stroke = new FinalNum(stroke);
        StrokeColors = strokeColors;
        ArrowHead = GetArrowHead();
    }

    private FinalTriangle GetArrowHead()
    {
        var vector = new Vector2(End.X.Value - Start.X.Value, End.Y.Value - Start.Y.Value);
        vector = Vector2.Normalize(vector);
        var orthoVector =  new Vector2(vector.Y, -vector.X);

        var basePointX = End.X.Value - vector.X * Stroke.Value;
        var basePointY = End.Y.Value - vector.Y * Stroke.Value;

        var point1x = basePointX - orthoVector.X * Stroke.Value;
        var point1y = basePointY - orthoVector.Y * Stroke.Value;
        var point1 = new FinalPoint(point1x , point1y);

        var point2x = basePointX + orthoVector.X * Stroke.Value;
        var point2y = basePointY + orthoVector.Y * Stroke.Value;
        var point2 = new FinalPoint(point2x , point2y);

        return new FinalTriangle(new FinalPoint(End.X.Value, End.Y.Value), [point1, point2], Stroke.Value, StrokeColors, StrokeColors);
    }
}