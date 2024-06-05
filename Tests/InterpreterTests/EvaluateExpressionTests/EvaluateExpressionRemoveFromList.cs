using GASLanguageProcessor.FinalTypes;

namespace Tests.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionRemoveFromList
{
    [Fact]
    public void EvaluateExpressionRemoveFromListNumPass()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<num> listNum = List<num>{1, 2, 3, 4, 5};" +
            "RemoveFromList(0, listNum);"
        );
        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);

        var result = sto.LookUp(envV.LookUp("listNum").Value) as FinalList;
        var expected = new FinalList(new List<object> { 2f, 3f, 4f, 5f });

        Assert.NotNull(result);
        Assert.IsType<FinalList>(result);
        Assert.Equal(expected.Values, result.Values);
    }

    [Fact]
    public void EvaluateExpressionRemoveFromListCirclePass()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<circle> listCircle = List<circle>" +
            "{" +
            "   Circle(Point(10, 20), 30, 10, Color(255, 0, 0, 1), Color(0, 255, 0, 1)), " +
            "   Circle(Point(100, 200), 30, 10, Color(0, 0, 255, 1), Color(0, 255, 0, 1)) " +
            "};" +
            "RemoveFromList(1, listCircle);"
        );
        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);

        var result = sto.LookUp(envV.LookUp("listCircle").Value) as FinalList;
        var expected = new FinalList(new List<object>
        {
            new FinalCircle(
                new FinalPoint(10, 20),
                30,
                10,
                new FinalColor(255, 0, 0, 1),
                new FinalColor(0, 255, 0, 1)
            )
        });

        Assert.NotNull(result);
        Assert.IsType<FinalList>(result);
        Assert.Equal(expected.Values.Count, result.Values.Count);
    }
}