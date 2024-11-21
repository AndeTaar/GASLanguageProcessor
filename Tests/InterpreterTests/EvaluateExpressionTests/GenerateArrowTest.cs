using GASLanguageProcessor.FinalTypes;

namespace Tests.InterpreterTests.EvaluateExpressionTests;

public class GenerateArrowTest
{
    [Fact]
    public void PassEvaluateGenerateArrow()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (150, 150, Color(255, 255, 255, 1));" +
            "arrow one = Arrow(Point(10,2), Point(20, 30), 10, Color(0,0,0,1));"
        );

        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);

        var result = sto.LookUp(envV.LookUp("one").Value) as FinalArrow;
        var expected = new FinalArrow(
            new FinalPoint(10, 2),
            new FinalPoint(20, 30),
            10,
            new FinalColor(0, 0, 0, 1)
        );

        Assert.NotNull(result);
        Assert.IsType<FinalArrow>(result);
        Assert.Equal(expected.Start.Y.Value, result.Start.Y.Value);
        Assert.Equal(expected.Start.X.Value, result.Start.X.Value);
        Assert.Equal(expected.End.Y.Value, result.End.Y.Value);
        Assert.Equal(expected.End.X.Value, result.End.X.Value);
        Assert.Equal(expected.Stroke.Value, result.Stroke.Value);
        Assert.Equal(expected.StrokeColor.Alpha.Value, result.StrokeColor.Alpha.Value);
        Assert.Equal(expected.StrokeColor.Red.Value, result.StrokeColor.Red.Value);
        Assert.Equal(expected.StrokeColor.Green.Value, result.StrokeColor.Green.Value);
        Assert.Equal(expected.StrokeColor.Blue.Value, result.StrokeColor.Blue.Value);

        var triangleResult = result.ArrowHead;
        var triangleExpected = expected.ArrowHead;

        Assert.Equal(triangleExpected.TrianglePeak.X.Value, triangleResult.TrianglePeak.X.Value);
        Assert.Equal(triangleExpected.TrianglePeak.Y.Value, triangleResult.TrianglePeak.Y.Value);
        Assert.Equal(triangleExpected.Points[0].X.Value, triangleResult.Points[0].X.Value);
        Assert.Equal(triangleExpected.Points[0].Y.Value, triangleResult.Points[0].Y.Value);
        Assert.Equal(triangleExpected.Points[1].X.Value, triangleResult.Points[1].X.Value);
        Assert.Equal(triangleExpected.Points[1].Y.Value, triangleResult.Points[1].Y.Value);
        Assert.Equal(triangleExpected.Stroke.Value, triangleResult.Stroke.Value);
        Assert.Equal(triangleExpected.Color.Alpha.Value, triangleResult.Color.Alpha.Value);
        Assert.Equal(triangleExpected.Color.Red.Value, triangleResult.Color.Red.Value);
        Assert.Equal(triangleExpected.Color.Green.Value, triangleResult.Color.Green.Value);
        Assert.Equal(triangleExpected.Color.Blue.Value, triangleResult.Color.Blue.Value);
        Assert.Equal(triangleExpected.StrokeColor.Alpha.Value, triangleResult.StrokeColor.Alpha.Value);
        Assert.Equal(triangleExpected.StrokeColor.Red.Value, triangleResult.StrokeColor.Red.Value);
        Assert.Equal(triangleExpected.StrokeColor.Green.Value, triangleResult.StrokeColor.Green.Value);
        Assert.Equal(triangleExpected.StrokeColor.Blue.Value, triangleResult.StrokeColor.Blue.Value);
    }
}