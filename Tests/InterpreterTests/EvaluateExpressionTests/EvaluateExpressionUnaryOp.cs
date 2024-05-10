using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionUnaryOp
{
    [Fact]
    public void PassEvaluateExpressionUnaryOpNegation()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (-125, -50, Color(255, 255, 255, 1));" +
                "number x = 20;" +
                "number y = -x;");
        var result = scope.vTable.LookUp("y")?.ActualValue;

        Assert.NotNull(result);
        Assert.Equal(-20f, result);
    }

    [Fact]
    public void PassEvaluateExpressionUnaryOpNot()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (125, 50, Color(255, 255, 255, 1)); " +
                "bool x = !true;");
        var result = scope.vTable.LookUp("x")?.ActualValue;

        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(false, result);
    }
}