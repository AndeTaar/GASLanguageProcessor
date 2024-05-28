namespace Tests.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionLengthOfList
{
    [Fact]
    public void EvaluateExpressionLengthOfListNumPass()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<num> listNum = List<num>{1, 2, 3, 4, 5};" +
            "num listLength = LengthOfList(listNum);"
        );

        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);
        
        var result = (float) sto.LookUp(envV.LookUp("listLength").Value);
        var expected = 5f;

        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void EvaluateExpressionLengthOfListCirclePass()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<circle> listCircle = List<circle>" +
            "{" +
            "   Circle(Point(10, 20), 30, 10, Color(255, 0, 0, 1), Color(0, 255, 0, 1)), " +
            "   Circle(Point(100, 200), 30, 10, Color(0, 0, 255, 1), Color(0, 255, 0, 1)) " +
            "};" +
            "num listLength = LengthOfList(listCircle);"
        );
        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);
        
        var result = (float) sto.LookUp(envV.LookUp("listLength").Value);
        var expected = 2f;

        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal(expected, result);
    }
}