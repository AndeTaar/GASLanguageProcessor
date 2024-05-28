using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class UnaryOpTest
{
    [Fact]
    public void PassEvaluateExpressionUnaryOpNegation()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (-125, -50, Color(255, 255, 255, 1));" +
                "num x = 20;" +
                "num y = -x;");

        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);
        
        var result = sto.LookUp(envV.LookUp("y").Value);

        Assert.NotNull(result);
        Assert.Equal(-20f, result);
    }

    [Fact]
    public void PassEvaluateExpressionUnaryOpNot()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (125, 50, Color(255, 255, 255, 1)); " +
                "bool x = !true;");

        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);
        
        var result = sto.LookUp(envV.LookUp("x").Value);

        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(false, result);
    }
}
