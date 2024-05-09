using System.Runtime.InteropServices.JavaScript;
using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateStatementTests;

public class EvaluateStatementDeclerationAssignment
{
    [Fact]
    public void PassEvaluateStatementDecleration()
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (250,250,Color(255,255,255,1));number x = 2;");
        var result = scope.vTable.LookUp("x")?.ActualValue;
        
        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal(2f, result);
    }
    
    [Fact]
    public void PassEvaluateStatementAssignment()
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (250,250,Color(255,255,255,1));number x = 2; x = 6");
        var result = scope.vTable.LookUp("x")?.ActualValue;
        
        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal(6f, result);
    }
}