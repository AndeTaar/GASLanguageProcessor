using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateStatementTests;

public class EvaluateReturnStatement
{
    [Fact]
    public void PassReturnStopsCompound()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "num test1;" +
            "void testFunction() {" +
            "test1 = 10;" +
            "return test1;" +
            "test1 = 20;" +
            "}" +
            "testFunction();"
        );
        var result = scope.vTable.LookUp("test1")?.ActualValue;

        Assert.NotNull(result);
        Assert.Equal(10f, result);
        Assert.NotEqual(20f, result);
    }
}