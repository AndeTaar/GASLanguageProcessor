namespace Tests.OperationalSemantics.InterpreterTests.EvaluateStatementTests;

public class EvaluateIfStatement
{
    [Fact]
    public void PassEvaluateIfStatement()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1)); \n" +
            "num x = 10; " +
            "if (x == 10) { x = 20; }");
        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);

        var result = sto.LookUp(envV.LookUp("x").Value);

        Assert.NotNull(result);
        Assert.Equal(20f, result);
    }

    [Fact]
    public void PassEvaluateIfElseStatement()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1)); \n" +
            "num x = 10; " +
            "if (x == 20) { x = 20; } else { x = 30; }");

        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);

        var result = sto.LookUp(envV.LookUp("x").Value);

        Assert.NotNull(result);
        Assert.Equal(30f, result);
    }

    [Fact]
    public void PassEvaluateIfElseIfStatement()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1)); \n" +
            "num x = 10; " +
            "if (x == 20) { x = 20; } else if (x == 10) { x = 30; }");

        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);

        var result = sto.LookUp(envV.LookUp("x").Value);

        Assert.NotNull(result);
        Assert.Equal(30f, result);
    }

    [Fact]
    public void PassEvaluateIfElseIfElseStatement()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1)); \n" +
            "num x = 10; " +
            "if (x == 20) { x = 20; } else if (x == 30) { x = 40; } else { x = 50; }");
        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);

        var result = sto.LookUp(envV.LookUp("x").Value);

        Assert.NotNull(result);
        Assert.Equal(50f, result);
    }
}