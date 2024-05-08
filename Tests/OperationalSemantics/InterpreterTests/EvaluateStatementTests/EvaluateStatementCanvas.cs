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
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (125, 55, Color(255, 255, 255, 1))") as Statement;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        var result = interpreter.EvaluateStatement(ast, scope) as FinalCanvas;
        
        Assert.NotNull(result);
        Assert.IsType<FinalCanvas>(result);
    }
    
    [Fact]
    public void PassEvaluateStatementCanvasWithoutColour()
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (125, 55)") as Statement;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        var result = interpreter.EvaluateStatement(ast, scope) as FinalCanvas;
        
        Assert.NotNull(result);
        Assert.IsType<FinalCanvas>(result);
    }
}