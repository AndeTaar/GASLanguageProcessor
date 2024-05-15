using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.FinalTypes;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateStatementTests;

public class EvaluateFunctionDeclarationAndCall
{
    [Fact]
    public void VisitPassVisitFunctionDeclarationWithLists()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<num> test(list<num> nums) {" +
            "   return nums;" +
            "}" +
            "list<num> nus = test(List<num>{1, 2, 3, 4, 5});"
        );
        var result = scope.vTable.LookUp("nus")?.ActualValue;
        Assert.NotNull(result);
        Assert.IsType<FinalList>(result);
        var finalList = (FinalList) result;
        Assert.Equal(5, finalList.Values.Count);
    }

    [Fact]
    public void VisitPassVisitFunctionDeclarationWithLists2()
    {
        var scope = SharedTesting.GetInterpretedScope(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1)); \n" +
            "list<circle> test(list<point> nums) { \n" +
            "   list<circle> circles = List<circle>{};\n" +
            "   for (num i = 0; i < LengthOfList(nums); i++) {\n" +
            "       AddToList(Circle(GetFromList(i, nums), 10, 10, Color(255,255,255,1), Color(255,255,255,1)), circles);\n" +
            "   }\n" +
            "   return circles;\n" +
            "}" +
            "list<circle> nus = test(List<point>{Point(10,10),Point(20,20),Point(30,30)});\n"
        );
        var result = scope.vTable.LookUp("nus")?.ActualValue;
        Assert.NotNull(result);
        Assert.IsType<FinalList>(result);
        var finalList = (FinalList) result;
        Assert.Equal(3, finalList.Values.Count);
    }
}