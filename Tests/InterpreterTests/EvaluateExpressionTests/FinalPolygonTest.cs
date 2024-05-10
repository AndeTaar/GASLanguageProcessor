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
        Assert.Equal(expected.Points, result.Points);
        Assert.Equal(expected.Stroke, result.Stroke);
        Assert.Equal(expected.Color, result.Color);
        Assert.Equal(expected.StrokeColor, result.StrokeColor);
    }
}
