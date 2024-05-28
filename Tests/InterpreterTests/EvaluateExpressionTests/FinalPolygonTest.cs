using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.FinalTypes;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class FinalPolygonTest
{
    [Fact]
    public void PassEvaluateFinalPolygon()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (150, 150, Colors(255, 255, 255, 1));" +
            "polygon one = Polygon(List<point>{Point(10,2), Point(20, 30), Point(40, 50)}, 10, Colors(255,0,255,1), Colors(255,255,0,1));"
            );

        var variable = scope.vTable.LookUp("one");
        var result = variable?.ActualValue as FinalPolygon;
        var expected = new FinalPolygon(
            new FinalList([ new FinalPoint(10, 2), new FinalPoint(20, 30), new FinalPoint(40, 50) ], scope),
            10,
            new FinalColors(255, 0, 255, 1),
            new FinalColors(255, 255, 0, 1)
        );

        Assert.NotNull(result);
        Assert.IsType<FinalPolygon>(result);
        for (int i = 0; i < expected.Points.Values.Count; i++)
        {
            var expectedPoint = expected.Points.Values[i] as FinalPoint;
            var resultPoint = result.Points.Values[i] as FinalPoint;
            Assert.Equal(expectedPoint?.X.Value, resultPoint?.X.Value);
            Assert.Equal(expectedPoint?.Y.Value, resultPoint?.Y.Value);
        }
        Assert.Equal(expected.Stroke.Value, result.Stroke.Value);
        Assert.Equal(expected.Colors.Alpha.Value, result.Colors.Alpha.Value);
        Assert.Equal(expected.Colors.Red.Value, result.Colors.Red.Value);
        Assert.Equal(expected.Colors.Green.Value, result.Colors.Green.Value);
        Assert.Equal(expected.Colors.Blue.Value, result.Colors.Blue.Value);
        Assert.Equal(expected.StrokeColors.Alpha.Value, result.StrokeColors.Alpha.Value);
        Assert.Equal(expected.StrokeColors.Red.Value, result.StrokeColors.Red.Value);
        Assert.Equal(expected.StrokeColors.Green.Value, result.StrokeColors.Green.Value);
        Assert.Equal(expected.StrokeColors.Blue.Value, result.StrokeColors.Blue.Value);
    }
}
