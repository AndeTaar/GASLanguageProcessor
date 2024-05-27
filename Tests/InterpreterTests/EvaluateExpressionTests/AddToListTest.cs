using GASLanguageProcessor;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class AddToListTest
{
    [Fact]
    public void EvaluateExpressionAddToListPass()
    {
        var scopeErrors = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "num x = 10;" +
            "list<num> l = List<num>{1, 2, 3, 4, 5};" +
            "AddToList(5, l);"
        );

        Assert.Empty(scopeErrors.Item2);
        var scope = scopeErrors.Item1;
        var result = scope.vTable.LookUp("l")?.ActualValue as FinalList;
        var expected = new FinalList(new List<object> { 1f,2f,3f,4f,5f,5f });

        Assert.NotNull(result);
        Assert.IsType<FinalList>(result);
        Assert.Equal(expected.Values, result.Values);
    }

    [Fact]
    public void EvaluateExpressionAddToListWithEmptyListPass()
    {
        var scopeErrors = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "num x = 10;" +
            "list<num> l = List<num>{};" +
            "AddToList(5, l);"
        );
        Assert.Empty(scopeErrors.Item2);
        var scope = scopeErrors.Item1;
        var result = scope.vTable.LookUp("l")?.ActualValue as FinalList;
        var expected = new FinalList(new List<object> { 5f });
        Assert.NotNull(result);
        Assert.IsType<FinalList>(result);
        Assert.Equal(expected.Values, result.Values);
    }
}
