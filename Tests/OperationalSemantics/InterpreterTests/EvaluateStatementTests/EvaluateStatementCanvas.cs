using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateStatementTests;

public class EvaluateStatementCanvas
{
 
    [Fact]
    public void PassEvaluateStatementCanvasWithColour()
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (125, 55, Color(255, 255, 255, 1))");
        var result = scope.vTable.LookUp("canvas")?.ActualValue;
        
        Assert.NotNull(result);
        Assert.IsType<FinalCanvas>(result);
    }
    
    [Fact]
    public void PassEvaluateStatementCanvasWithoutColour()
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (125, 55)");
        var result = scope.vTable.LookUp("canvas")?.ActualValue;
        
        Assert.NotNull(result);
        Assert.IsType<FinalCanvas>(result);
    }
}