using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateStatementTests;

public class EvaluateReturnStatement
{
    [Fact]
    public void PassReturnStopsCompound()
    {
        var scopeErrors = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "num test1;" +
            "void testFunction() {" +
            "test1 = 10;" +
            "return test1;" +
            "test1 = 20;" +
            "}" +
            "testFunction();"
        );

        Assert.Empty(scopeErrors.Item2);
        var scope = scopeErrors.Item1;

        var result = scope.vTable.LookUp("test1")?.ActualValue;

        Assert.NotNull(result);
        Assert.Equal(10f, result);
        Assert.NotEqual(20f, result);
    }
}