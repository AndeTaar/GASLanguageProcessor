using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionGetFromList
{
    [Fact]
    public void EvaluateExpressionGetFromListPass()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<number> listOne = List{1, 2, 3, 4, 5};" +
            "number fromList = GetFromList(0, listOne);"
        );
        var result = scope.vTable.LookUp("fromList")?.ActualValue;
        var expected = 1f;

        Assert.NotNull(result);
        Assert.IsType<Number>(result);
        Assert.Equal(expected, result);
    }
    
}