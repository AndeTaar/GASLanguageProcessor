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
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
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
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<circle> listCircle = List<circle>" +
            "{" +
            "   Circle(Point(10, 20), 30, 10, Color(255, 0, 0, 1), Color(0, 255, 0, 1)), " + 
            "   Circle(Point(100, 200), 30, 10, Color(0, 0, 255, 1), Color(0, 255, 0, 1)) " +
            "};" +
            "circle fromList = GetFromList(1, listCircle);"
        );
        var result = scope.vTable.LookUp("fromList")?.ActualValue as FinalCircle;
        
        var expected =
            new FinalCircle(
                new FinalPoint(100f, 200f),
                30f,
                10f,
                new FinalColor(0f, 0f, 255f, 1f),
                new FinalColor(0f, 255f, 0f, 1f)
        );

        Assert.NotNull(result);
        Assert.IsType<FinalCircle>(result);
        
        Assert.Equal(expected.Center.X.Value, result.Center.X.Value);
        Assert.Equal(expected.Center.Y.Value, result.Center.Y.Value);
        Assert.Equal(expected.Radius.Value, result.Radius.Value);
        Assert.Equal(expected.Stroke.Value, result.Stroke.Value);
        
        Assert.Equal(expected.FillColor.Red.Value, result.FillColor.Red.Value);
        Assert.Equal(expected.FillColor.Green.Value, result.FillColor.Green.Value);
        Assert.Equal(expected.FillColor.Blue.Value, result.FillColor.Blue.Value);
        Assert.Equal(expected.FillColor.Alpha.Value, result.FillColor.Alpha.Value);
        
        Assert.Equal(expected.StrokeColor.Red.Value, result.StrokeColor.Red.Value);
        Assert.Equal(expected.StrokeColor.Green.Value, result.StrokeColor.Green.Value);
        Assert.Equal(expected.StrokeColor.Blue.Value, result.StrokeColor.Blue.Value);
        Assert.Equal(expected.StrokeColor.Alpha.Value, result.StrokeColor.Alpha.Value);
        
    }
    
    
}