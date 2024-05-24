namespace Tests.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionLengthOfList
{
    [Fact]
    public void EvaluateExpressionLengthOfListNumPass()
    {
        var scopeErrors = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<num> listNum = List<num>{1, 2, 3, 4, 5};" +
            "num listLength = LengthOfList(listNum);"
        );

        Assert.Empty(scopeErrors.Item2);
        var scope = scopeErrors.Item1;

        var result = (float) scope.vTable.LookUp("listLength")?.ActualValue;
        var expected = 5f;

        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void EvaluateExpressionLengthOfListCirclePass()
    {
        var scopeErrors = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<circle> listCircle = List<circle>" +
            "{" +
            "   Circle(Point(10, 20), 30, 10, Color(255, 0, 0, 1), Color(0, 255, 0, 1)), " +
            "   Circle(Point(100, 200), 30, 10, Color(0, 0, 255, 1), Color(0, 255, 0, 1)) " +
            "};" +
            "num listLength = LengthOfList(listCircle);"
        );
        Assert.Empty(scopeErrors.Item2);
        var scope = scopeErrors.Item1;
        var result = (float) scope.vTable.LookUp("listLength")?.ActualValue;
        var expected = 2f;

        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal(expected, result);
    }
}