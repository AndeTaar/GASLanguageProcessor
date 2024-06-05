using GASLanguageProcessor.FinalTypes;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class FunctionCallTest
{
    [Fact]
    public void PassEvaluateExpressionFunctionCall()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (125, 50, Color(255, 255, 255, 1)); " +
            "num Func(num x) {return x+20*5;}" +
            "num funcCallVal = Func(0);");

        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);

        var result = sto.LookUp(envV.LookUp("funcCallVal").Value);

        Assert.NotNull(result);
        Assert.Equal(100f, result);
    }

    [Fact]
    public void PassEvaluateExpressionFunctionCallWithMultipleArguments()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (125, 50, Color(255, 255, 255, 1)); " +
            "num Func(num x, num y) {return x+y;}" +
            "num funcCallVal = Func(10, 20);" +
            "color white = Color(255, 255, 255, 1);" +
            "point p = Point(10, 20);" +
            "square s = Square(p, 10, 20, white, white,1);");

        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);

        var result = sto.LookUp(envV.LookUp("funcCallVal").Value);

        Assert.NotNull(result);

        Assert.Equal(30f, result);

        var square = sto.LookUp(envV.LookUp("s").Value);
        Assert.IsAssignableFrom<FinalSquare>(square);
        var finalSquare = (FinalSquare)square;
        Assert.Equal(10f, finalSquare.Length.Value);
        Assert.Equal(20f, finalSquare.Stroke.Value);

        var topLeft = finalSquare.TopLeft;
        Assert.IsAssignableFrom<FinalPoint>(topLeft);
        Assert.Equal(10f, topLeft.X.Value);
        Assert.Equal(20f, topLeft.Y.Value);

        Assert.IsAssignableFrom<FinalColor>(finalSquare.FillColor);
        Assert.IsAssignableFrom<FinalColor>(finalSquare.StrokeColor);

        var white = sto.LookUp(envV.LookUp("white").Value);
        Assert.IsAssignableFrom<FinalColor>(white);
        var finalColor = (FinalColor)white;
        Assert.Equal(255, finalColor.Red.Value);
        Assert.Equal(255, finalColor.Green.Value);
        Assert.Equal(255, finalColor.Blue.Value);
        Assert.Equal(1, finalColor.Alpha.Value);
    }
}