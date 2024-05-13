using GASLanguageProcessor.FinalTypes;

namespace Tests.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionRemoveFromList
{
    [Fact]
    public void EvaluateExpressionRemoveFromListNumberPass()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<number> listNumber = List{1, 2, 3, 4, 5};" +
            "RemoveFromList(0, listNumber);"
        );
        var result = scope.vTable.LookUp("listNumber")?.ActualValue as FinalList;
        var expected = new FinalList(new List<object> {2f, 3f, 4f, 5f}, scope);

        Assert.NotNull(result);
        Assert.IsType<FinalList>(result);
        Assert.Equal(expected.Values, result.Values);
    }
    
    [Fact]
    public void EvaluateExpressionRemoveFromListCirclePass()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<circle> listCircle = List" +
            "{" +
            "   Circle(Point(10, 20), 30, 10, Color(255, 0, 0, 1), Color(0, 255, 0, 1)), " + 
            "   Circle(Point(100, 200), 30, 10, Color(0, 0, 255, 1), Color(0, 255, 0, 1)) " +
            "};" +
            "RemoveFromList(1, listCircle);"
        );
        var result = scope.vTable.LookUp("listCircle")?.ActualValue as FinalList;
        var expected = new FinalList(new List<object>
        {
            new FinalCircle(
                new FinalPoint(10, 20),
                30,
                10,
                new FinalColor(255, 0, 0, 1),
                new FinalColor(0, 255, 0, 1)
            )
        }, scope);

        Assert.NotNull(result);
        Assert.IsType<FinalList>(result);
        Assert.Equal(expected.Values.Count, result.Values.Count);
    }
}