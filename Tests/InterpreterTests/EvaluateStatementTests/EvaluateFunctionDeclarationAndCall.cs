using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.FinalTypes;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateStatementTests;

public class EvaluateFunctionDeclarationAndCall
{
    [Fact]
    public void VisitPassVisitFunctionDeclarationWithLists()
    {
        var env = SharedTesting.RunInterpreter(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<num> test(list<num> nums) {" +
            "   return nums;" +
            "}" +
            "list<num> nus = test(List<num>{1, 2, 3, 4, 5});"
        );

        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);
        
        var result = sto.LookUp(envV.LookUp("nus").Value);
        Assert.NotNull(result);
        Assert.IsType<FinalList>(result);
        var finalList = (FinalList) result;
        Assert.Equal(5, finalList.Values.Count);
    }

    [Fact]
    public void VisitPassVisitFunctionDeclarationWithLists2()
    {
        var env = SharedTesting.RunInterpreter(
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

        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);
        
        var result = sto.LookUp(envV.LookUp("nus").Value);
        Assert.NotNull(result);
        Assert.IsType<FinalList>(result);
        var finalList = (FinalList) result;
        Assert.Equal(3, finalList.Values.Count);
    }
}