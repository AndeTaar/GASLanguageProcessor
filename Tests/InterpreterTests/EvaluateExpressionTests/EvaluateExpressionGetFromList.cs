using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionGetFromList
{
    [Fact]
    public void EvaluateExpressionGetFromListNumPass()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "list<num> listNum = List<num>{1, 2, 3, 4, 5};" +
            "num fromList = GetFromList(0, listNum);"
        );
        var result = scope.vTable.LookUp("fromList")?.ActualValue;
        var expected = 1f;

        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void EvaluateExpressionGetFromListCirclePass()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "list<circle> listCircle = List<circle>" +
            "{" +
            "   Circle(Point(10, 20), 30, 10, Colors(255, 0, 0, 1), Colors(0, 255, 0, 1)), " + 
            "   Circle(Point(100, 200), 30, 10, Colors(0, 0, 255, 1), Colors(0, 255, 0, 1)) " +
            "};" +
            "circle fromList = GetFromList(1, listCircle);"
        );
        var result = scope.vTable.LookUp("fromList")?.ActualValue as FinalCircle;
        
        var expected =
            new FinalCircle(
                new FinalPoint(100f, 200f),
                30f,
                10f,
                new FinalColors(0f, 0f, 255f, 1f),
                new FinalColors(0f, 255f, 0f, 1f)
        );

        Assert.NotNull(result);
        Assert.IsType<FinalCircle>(result);
        
        Assert.Equal(expected.Center.X.Value, result.Center.X.Value);
        Assert.Equal(expected.Center.Y.Value, result.Center.Y.Value);
        Assert.Equal(expected.Radius.Value, result.Radius.Value);
        Assert.Equal(expected.Stroke.Value, result.Stroke.Value);
        
        Assert.Equal(expected.FillColors.Red.Value, result.FillColors.Red.Value);
        Assert.Equal(expected.FillColors.Green.Value, result.FillColors.Green.Value);
        Assert.Equal(expected.FillColors.Blue.Value, result.FillColors.Blue.Value);
        Assert.Equal(expected.FillColors.Alpha.Value, result.FillColors.Alpha.Value);
        
        Assert.Equal(expected.StrokeColors.Red.Value, result.StrokeColors.Red.Value);
        Assert.Equal(expected.StrokeColors.Green.Value, result.StrokeColors.Green.Value);
        Assert.Equal(expected.StrokeColors.Blue.Value, result.StrokeColors.Blue.Value);
        Assert.Equal(expected.StrokeColors.Alpha.Value, result.StrokeColors.Alpha.Value);
        
    }
    
    
}