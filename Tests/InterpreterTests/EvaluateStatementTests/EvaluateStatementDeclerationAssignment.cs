namespace Tests.OperationalSemantics.InterpreterTests.EvaluateStatementTests;

public class EvaluateStatementDeclerationAssignment
{
    [Fact]
    public void PassEvaluateStatementDecleration()
    {
        var env = SharedTesting.RunInterpreter("canvas (250,250,Color(255,255,255,1));num x = 2;");
        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);

        var result = sto.LookUp(envV.LookUp("x").Value);

        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal(2f, result);
    }

    [Fact]
    public void PassEvaluateStatementAssignment()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (250,250,Color(255,255,255,1));" +
            "num x = 2; " +
            "x = 6;");
        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);

        var result = sto.LookUp(envV.LookUp("x").Value);

        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal(6f, result);
    }
}