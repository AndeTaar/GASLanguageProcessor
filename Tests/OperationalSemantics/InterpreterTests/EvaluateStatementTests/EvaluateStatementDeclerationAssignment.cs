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
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (250,250,Color(255,255,255,1));number x = 2;") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("x", new Variable("x", GasType.Number));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope);
        
        Assert.NotNull(result);
        Assert.IsType<Declaration>(ast.Statement2);
        Assert.IsType<float>(result);
        Assert.Equal((float)2, result);
    }
    
    [Fact]
    public void PassEvaluateStatementAssignment()
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (250,250,Color(255,255,255,1));number x = 2; x = 6") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("x", new Variable("x", GasType.Number));
        var compound = ast.Statement2 as Compound;
        var result = interpreter.EvaluateStatement(compound.Statement2, scope);
        
        Assert.NotNull(result);
        Assert.IsType<Assignment>(compound.Statement2);
        Assert.IsType<float>(result);
        Assert.Equal((float)6, result);
    }
}