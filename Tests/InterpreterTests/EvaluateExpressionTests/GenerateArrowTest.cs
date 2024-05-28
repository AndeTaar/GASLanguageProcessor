using GASLanguageProcessor.FinalTypes;

namespace Tests.InterpreterTests.EvaluateExpressionTests;

public class GenerateArrowTest
{
    [Fact]
public void PassEvaluateGenerateArrow()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (150, 150, Colors(255, 255, 255, 1));" +
            "arrow one = Arrow(Point(10,2), Point(20, 30), 10, Colors(0,0,0,1));"
            );

        var variable = scope.vTable.LookUp("one");
        var result = variable?.ActualValue as FinalArrow;
        var expected = new FinalArrow(
            new FinalPoint(10, 2),
            new FinalPoint(20, 30),
            10,
            new FinalColors(0, 0, 0, 1)
        );

        Assert.NotNull(result);
        Assert.IsType<FinalArrow>(result);
        Assert.Equal(expected.Start.Y.Value, result.Start.Y.Value);
        Assert.Equal(expected.Start.X.Value, result.Start.X.Value);
        Assert.Equal(expected.End.Y.Value, result.End.Y.Value);
        Assert.Equal(expected.End.X.Value, result.End.X.Value);
        Assert.Equal(expected.Stroke.Value, result.Stroke.Value);
        Assert.Equal(expected.StrokeColors.Alpha.Value, result.StrokeColors.Alpha.Value);
        Assert.Equal(expected.StrokeColors.Red.Value, result.StrokeColors.Red.Value);
        Assert.Equal(expected.StrokeColors.Green.Value, result.StrokeColors.Green.Value);
        Assert.Equal(expected.StrokeColors.Blue.Value, result.StrokeColors.Blue.Value);
        
        var triangleResult = result.ArrowHead;
        var triangleExpected = expected.ArrowHead;
        
        Assert.Equal(triangleExpected.TrianglePeak.X.Value, triangleResult.TrianglePeak.X.Value);
        Assert.Equal(triangleExpected.TrianglePeak.Y.Value, triangleResult.TrianglePeak.Y.Value);
        Assert.Equal(triangleExpected.Points[0].X.Value, triangleResult.Points[0].X.Value);
        Assert.Equal(triangleExpected.Points[0].Y.Value, triangleResult.Points[0].Y.Value);
        Assert.Equal(triangleExpected.Points[1].X.Value, triangleResult.Points[1].X.Value);
        Assert.Equal(triangleExpected.Points[1].Y.Value, triangleResult.Points[1].Y.Value);
        Assert.Equal(triangleExpected.Stroke.Value, triangleResult.Stroke.Value);
        Assert.Equal(triangleExpected.Colors.Alpha.Value, triangleResult.Colors.Alpha.Value);
        Assert.Equal(triangleExpected.Colors.Red.Value, triangleResult.Colors.Red.Value);
        Assert.Equal(triangleExpected.Colors.Green.Value, triangleResult.Colors.Green.Value);
        Assert.Equal(triangleExpected.Colors.Blue.Value, triangleResult.Colors.Blue.Value);
        Assert.Equal(triangleExpected.StrokeColors.Alpha.Value, triangleResult.StrokeColors.Alpha.Value);
        Assert.Equal(triangleExpected.StrokeColors.Red.Value, triangleResult.StrokeColors.Red.Value);
        Assert.Equal(triangleExpected.StrokeColors.Green.Value, triangleResult.StrokeColors.Green.Value);
        Assert.Equal(triangleExpected.StrokeColors.Blue.Value, triangleResult.StrokeColors.Blue.Value);
    }
    
}