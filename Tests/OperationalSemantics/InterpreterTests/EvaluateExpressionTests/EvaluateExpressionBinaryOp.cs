using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionBinaryOp
{
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpPlusNumber() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (125+25, 55+94, Color(255, 255, 255, 1))") as Statement;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        var expected = new FinalCanvas(150, 149, new FinalColor(255, 255, 255, 1));
        var result = interpreter.EvaluateStatement(ast, scope) as FinalCanvas;
        
        Assert.NotNull(result);
        Assert.IsType<FinalCanvas>(result);
        Assert.IsType<FinalCanvas>(expected);
        Assert.Equal(expected.Height, result.Height);
        Assert.Equal(expected.Width, result.Width);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpPlusString() //Unfinished test since string concatenation is not implemented correctly
    {
        var typeCheckingVisitor = new TypeCheckingAstVisitor();
        var scopeCheckingVisitor = new ScopeCheckingAstVisitor();
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (250,250,Color(255,255,255,1));string Hello = \"Hello\" + \" \" + \"World\";") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("Hello", new Variable("Hello", GasType.String));
        ast.Accept(scopeCheckingVisitor);
        ast.Accept(typeCheckingVisitor);
        var result = interpreter.EvaluateStatement(ast.Statement2, scope);
        
        Assert.NotNull(result);
        Assert.IsType<string>(result);
        Assert.Equal("Hello World", result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpMinus()//canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (125-25, 55-54, Color(255, 255, 255, 1))") as Statement;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        var expected = new FinalCanvas(100, 1, new FinalColor(255, 255, 255, 1));
        var result = interpreter.EvaluateStatement(ast, scope) as FinalCanvas;
        
        Assert.NotNull(result);
        Assert.IsType<FinalCanvas>(result);
        Assert.IsType<FinalCanvas>(expected);
        Assert.Equal(expected.Height, result.Height);
        Assert.Equal(expected.Width, result.Width);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpMultiply()//canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (125*2, 50*4, Color(255, 255, 255, 1))") as Statement;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        var expected = new FinalCanvas(250, 200, new FinalColor(255, 255, 255, 1));
        var result = interpreter.EvaluateStatement(ast, scope) as FinalCanvas;
        
        Assert.NotNull(result);
        Assert.IsType<FinalCanvas>(result);
        Assert.IsType<FinalCanvas>(expected);
        Assert.Equal(expected.Height, result.Height);
        Assert.Equal(expected.Width, result.Width);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpDivision()//canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (125/2, 50/2, Color(255, 255, 255, 1))") as Statement;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        var expected = new FinalCanvas((float)62.5, 25, new FinalColor(255, 255, 255, 1));
        var result = interpreter.EvaluateStatement(ast, scope) as FinalCanvas;
        
        Assert.NotNull(result);
        Assert.IsType<FinalCanvas>(result);
        Assert.IsType<FinalCanvas>(expected);
        Assert.Equal(expected.Height, result.Height);
        Assert.Equal(expected.Width, result.Width);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpModulus()//canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (125%2, 50%4, Color(255, 255, 255, 1))") as Statement;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        var expected = new FinalCanvas(1, 2, new FinalColor(255, 255, 255, 1));
        var result = interpreter.EvaluateStatement(ast, scope) as FinalCanvas;
        
        Assert.NotNull(result);
        Assert.IsType<FinalCanvas>(result);
        Assert.IsType<FinalCanvas>(expected);
        Assert.Equal(expected.Height, result.Height);
        Assert.Equal(expected.Width, result.Width);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpLessThan()//canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (250, 200, Color(255, 255, 255, 1)); bool x = 20<30;") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("x", new Variable("x", GasType.Boolean));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope);
        
        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(true, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpGreaterThan()//canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (250, 200, Color(255, 255, 255, 1)); bool x = 20>30;") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("x", new Variable("x", GasType.Boolean));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope);
        
        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(false, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpLessThanOrEqual()//canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (250, 200, Color(255, 255, 255, 1)); bool x = 20<=30;") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("x", new Variable("x", GasType.Boolean));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope);
        
        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(true, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpGreaterThanOrEqual()//canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (250, 200, Color(255, 255, 255, 1)); bool x = 20>=30;") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("x", new Variable("x", GasType.Boolean));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope);
        
        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(false, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpAnd()//canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (250, 200, Color(255, 255, 255, 1)); bool x = true && false;") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("x", new Variable("x", GasType.Boolean));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope);
        
        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(false, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpOr()//canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (250, 200, Color(255, 255, 255, 1)); bool x = true || false;") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("x", new Variable("x", GasType.Boolean));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope);
        
        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(true, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpEqual()//canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (250, 200, Color(255, 255, 255, 1)); bool x = 20 == 40;") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("x", new Variable("x", GasType.Boolean));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope);
        
        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(false, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpNotEqual()//canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (250, 200, Color(255, 255, 255, 1)); bool x = 20 != 40;") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("x", new Variable("x", GasType.Boolean));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope);
        
        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(true, result);
    }
}