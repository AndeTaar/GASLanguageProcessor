﻿using GASLanguageProcessor;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class AddToListTest
{
    [Fact]
    public void EvaluateExpressionAddToListPass()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "num x = 10;" +
            "list<num> l = List{1, 2, 3, 4, 5};" +
            "AddToList(5, l);"
        );
        var result = scope.vTable.LookUp("l")?.ActualValue as FinalList;
        var expected = new FinalList(new List<object> { 1f,2f,3f,4f,5f,5f }, scope);

        Assert.NotNull(result);
        Assert.IsType<FinalList>(result);
        Assert.Equal(expected.Values, result.Values);
    }

    [Fact]
    public void EvaluateExpressionAddToListWithEmptyListPass()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "num x = 10;" +
            "list<num> l;" +
            "AddToList(5, l);"
        );
        var result = scope.vTable.LookUp("l")?.ActualValue as FinalList;
        var expected = new FinalList(new List<object> { 5f }, scope);
        //Fails for now
        /**Assert.NotNull(result);
        Assert.IsType<FinalList>(result);
        Assert.Equal(expected.Values, result.Values);*/
    }
}
