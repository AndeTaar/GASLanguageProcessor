namespace Tests.OperationalSemantics.InterpreterTests.EvaluateStatementTests;

public class EvaluateIfStatement
{
    [Fact]
    public void PassEvaluateIfStatement()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1)); \n" +
            "num x = 10; " +
            "if (x == 10) { x = 20; }");
        var result = scope.vTable.LookUp("x")?.ActualValue;

        Assert.NotNull(result);
        Assert.Equal(20f, result);
    }
    
    [Fact]
    public void PassEvaluateIfElseStatement()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1)); \n" +
            "num x = 10; " +
            "if (x == 20) { x = 20; } else { x = 30; }");
        var result = scope.vTable.LookUp("x")?.ActualValue;

        Assert.NotNull(result);
        Assert.Equal(30f, result);
    }
    
    [Fact]
    public void PassEvaluateIfElseIfStatement()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1)); \n" +
            "num x = 10; " +
            "if (x == 20) { x = 20; } else if (x == 10) { x = 30; }");
        var result = scope.vTable.LookUp("x")?.ActualValue;

        Assert.NotNull(result);
        Assert.Equal(30f, result);
    }
    
    [Fact]
    public void PassEvaluateIfElseIfElseStatement()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1)); \n" +
            "num x = 10; " +
            "if (x == 20) { x = 20; } else if (x == 30) { x = 40; } else { x = 50; }");
        var result = scope.vTable.LookUp("x")?.ActualValue;

        Assert.NotNull(result);
        Assert.Equal(50f, result);
    }
    
}