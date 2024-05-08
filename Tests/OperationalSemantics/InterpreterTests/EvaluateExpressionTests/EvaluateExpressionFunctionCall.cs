using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionFunctionCall
{
    [Fact]
    public void PassEvaluateExpressionFunctionCall()
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (125, 50, Color(255, 255, 255, 1)); " +
                                                      "number Func(number x) {return x+20*5;}" +
                                                      "number funcCallVal = Func(0);");
        var result = scope.vTable.LookUp("funcCallVal")?.ActualValue;
        
        Assert.NotNull(result);
        Assert.Equal(100f, result);
    }
}