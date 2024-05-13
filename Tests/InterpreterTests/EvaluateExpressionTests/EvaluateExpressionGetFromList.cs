using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionGetFromList
{
    [Fact]
    public void EvaluateExpressionGetFromListNumberPass()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<number> listNumber = List{1, 2, 3, 4, 5};" +
            "number fromList = GetFromList(0, listNumber);"
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
            "list<circle> listCircle = List" +
            "{" +
            "   Circle(Point(10, 20), 30, 10, Color(255, 0, 0, 1), Color(0, 255, 0, 1)), " + 
            "   Circle(Point(100, 200), 30, 10, Color(0, 0, 255, 1), Color(0, 255, 0, 1)) " +
            "};" +
            "circle fromList = GetFromList(1, listCircle);"
        );
        var result = scope.vTable.LookUp("fromList")?.ActualValue as FinalCircle;
        
        var expected =
            new FinalCircle(
                new FinalPoint(100, 200),
                30,
                10,
                new FinalColor(0, 0, 255, 1),
                new FinalColor(0, 255, 0, 1)
        );

        Assert.NotNull(result);
        Assert.IsType<FinalCircle>(result);
        
        Assert.Equal(expected.Center.X, result.Center.X);
        Assert.Equal(expected.Center.Y, result.Center.Y);
        Assert.Equal(expected.Radius, result.Radius);
        Assert.Equal(expected.Stroke, result.Stroke);
        
        Assert.Equal(expected.FillColor.Red, result.FillColor.Red);
        Assert.Equal(expected.FillColor.Green, result.FillColor.Green);
        Assert.Equal(expected.FillColor.Blue, result.FillColor.Blue);
        Assert.Equal(expected.FillColor.Alpha, result.FillColor.Alpha);
        
        Assert.Equal(expected.StrokeColor.Red, result.StrokeColor.Red);
        Assert.Equal(expected.StrokeColor.Green, result.StrokeColor.Green);
        Assert.Equal(expected.StrokeColor.Blue, result.StrokeColor.Blue);
        Assert.Equal(expected.StrokeColor.Alpha, result.StrokeColor.Alpha);
        
    }
    
    
}