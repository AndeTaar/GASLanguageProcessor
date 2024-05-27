using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateStatementTests;

public class EvaluateReturnStatement
{
    [Fact]
    public void PassReturnStopsCompound()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "num test1;" +
            "void testFunction() {" +
            "test1 = 10;" +
            "return test1;" +
            "test1 = 20;" +
            "}" +
            "testFunction();"
        );

        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);
        
        var result = sto.LookUp(envV.LookUp("test1").Value);

        Assert.NotNull(result);
        Assert.Equal(10f, result);
        Assert.NotEqual(20f, result);
    }
}