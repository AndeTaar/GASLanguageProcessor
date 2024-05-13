namespace Tests.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionLengthOfList
{
    [Fact]
    public void EvaluateExpressionLengthOfListNumberPass()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<number> listNumber = List{1, 2, 3, 4, 5};" +
            "number listLength = LengthOfList(listNumber);"
        );
        var result = (float) scope.vTable.LookUp("listLength")?.ActualValue;
        var expected = 5f;

        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void EvaluateExpressionLengthOfListCirclePass()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<circle> listCircle = List" +
            "{" +
            "   Circle(Point(10, 20), 30, 10, Color(255, 0, 0, 1), Color(0, 255, 0, 1)), " + 
            "   Circle(Point(100, 200), 30, 10, Color(0, 0, 255, 1), Color(0, 255, 0, 1)) " +
            "};" +
            "number listLength = LengthOfList(listCircle);"
        );
        var result = (float) scope.vTable.LookUp("listLength")?.ActualValue;
        var expected = 2f;

        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal(expected, result);
    }
}