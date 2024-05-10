using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.FinalTypes;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class FinalPolygonTest
{
    [Fact]
    public void PassEvaluateFinalPolygon()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (150, 150, Color(255, 255, 255, 1));" +
            "polygon one = Polygon(List{Point(10,2), Point(20, 30), Point(40, 50)}, 10, Color(255,0,255,1), Color(255,255,0,1));"
            );

        var variable = scope.vTable.LookUp("one");
        var result = variable?.ActualValue as FinalPolygon;
        var expected = new FinalPolygon(
            new FinalList([ new FinalPoint(10, 2), new FinalPoint(20, 30), new FinalPoint(40, 50) ], scope),
            10,
            new FinalColor(255, 0, 255, 1),
            new FinalColor(255, 255, 0, 1)
        );

        Assert.NotNull(result);
        Assert.IsType<FinalPolygon>(result);
        for (int i = 0; i < expected.Points.Values.Count; i++)
        {
            var expectedPoint = expected.Points.Values[i] as FinalPoint;
            var resultPoint = result.Points.Values[i] as FinalPoint;
            Assert.Equal(expectedPoint?.X, resultPoint?.X);
            Assert.Equal(expectedPoint?.Y, resultPoint?.Y);
        }
        Assert.Equal(expected.Stroke, result.Stroke);
        Assert.Equal(expected.Color.Alpha, result.Color.Alpha);
        Assert.Equal(expected.Color.Red, result.Color.Red);
        Assert.Equal(expected.Color.Green, result.Color.Green);
        Assert.Equal(expected.Color.Blue, result.Color.Blue);
        Assert.Equal(expected.StrokeColor.Alpha, result.StrokeColor.Alpha);
        Assert.Equal(expected.StrokeColor.Red, result.StrokeColor.Red);
        Assert.Equal(expected.StrokeColor.Green, result.StrokeColor.Green);
        Assert.Equal(expected.StrokeColor.Blue, result.StrokeColor.Blue);
    }
}
