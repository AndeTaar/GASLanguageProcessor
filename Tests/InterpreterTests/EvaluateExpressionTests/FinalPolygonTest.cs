using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.FinalTypes;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class FinalPolygonTest
{
    [Fact]
    public void PassEvaluateFinalPolygon()
    {
        var scopeErrors = SharedTesting.GetInterpretedScope(
            "canvas (150, 150, Color(255, 255, 255, 1));" +
            "polygon one = Polygon(List<point>{Point(10,2), Point(20, 30), Point(40, 50)}, 10, Color(255,0,255,1), Color(255,255,0,1));"
            );

        Assert.Empty(scopeErrors.Item2);
        var scope = scopeErrors.Item1;

        var variable = scope.vTable.LookUp("one");
        var result = variable?.ActualValue as FinalPolygon;
        var expected = new FinalPolygon(
            new FinalList([ new FinalPoint(10, 2), new FinalPoint(20, 30), new FinalPoint(40, 50) ]),
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
            Assert.Equal(expectedPoint?.X.Value, resultPoint?.X.Value);
            Assert.Equal(expectedPoint?.Y.Value, resultPoint?.Y.Value);
        }
        Assert.Equal(expected.Stroke.Value, result.Stroke.Value);
        Assert.Equal(expected.Color.Alpha.Value, result.Color.Alpha.Value);
        Assert.Equal(expected.Color.Red.Value, result.Color.Red.Value);
        Assert.Equal(expected.Color.Green.Value, result.Color.Green.Value);
        Assert.Equal(expected.Color.Blue.Value, result.Color.Blue.Value);
        Assert.Equal(expected.StrokeColor.Alpha.Value, result.StrokeColor.Alpha.Value);
        Assert.Equal(expected.StrokeColor.Red.Value, result.StrokeColor.Red.Value);
        Assert.Equal(expected.StrokeColor.Green.Value, result.StrokeColor.Green.Value);
        Assert.Equal(expected.StrokeColor.Blue.Value, result.StrokeColor.Blue.Value);
    }
}
