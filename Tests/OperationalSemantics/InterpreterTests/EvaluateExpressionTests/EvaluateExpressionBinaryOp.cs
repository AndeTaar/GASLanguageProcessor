using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionBinaryOp
{
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpPlusNumber()
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
    public void PassEvaluateExpressionBinaryOpPlusString() //Unfinished test
    {
        var visitor = new ToAstVisitor();
        var interpreter = new Interpreter();
        var inputStream = new AntlrInputStream(
            "\"Hello\" + \"World\"");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var expression = visitor.VisitExpression(parser.expression()) as Expression;
        var scope = new Scope(null, null);
        var result = interpreter.EvaluateExpression(expression, scope);


        Assert.NotNull(result);
        Assert.IsType<String>(result);
        Assert.Equal("Hello World", result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpMinus()
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
    public void PassEvaluateExpressionBinaryOpMultiply()
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
    
}