using System.Numerics;
using GASLanguageProcessor.FinalTypes.Colors;

namespace GASLanguageProcessor.FinalTypes;

public class FinalArrow : FinalType
{
    public FinalArrow(FinalPoint start, FinalPoint end, float stroke, FinalColors strokeColor)
    {
        Start = start;
        End = end;
        Stroke = new FinalNum(stroke);
        StrokeColor = strokeColor;
        ArrowHead = GetArrowHead();
    }

    public FinalPoint Start { get; set; }
    public FinalPoint End { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColors StrokeColor { get; set; }
    public FinalTriangle ArrowHead { get; set; }

    private FinalTriangle GetArrowHead()
    {
        var vector = new Vector2(End.X.Value - Start.X.Value, End.Y.Value - Start.Y.Value);
        vector = Vector2.Normalize(vector);
        var orthoVector = new Vector2(vector.Y, -vector.X);

        var basePointX = End.X.Value - vector.X * Stroke.Value;
        var basePointY = End.Y.Value - vector.Y * Stroke.Value;

        var point1x = basePointX - orthoVector.X * Stroke.Value;
        var point1y = basePointY - orthoVector.Y * Stroke.Value;
        var point1 = new FinalPoint(point1x, point1y);

        var point2x = basePointX + orthoVector.X * Stroke.Value;
        var point2y = basePointY + orthoVector.Y * Stroke.Value;
        var point2 = new FinalPoint(point2x, point2y);

        var finalList = new FinalList(new object[] {point1, point2, new FinalPoint(End.X.Value, End.Y.Value)});

        return new FinalTriangle(finalList, Stroke.Value, StrokeColor,
            StrokeColor);
    }
}
