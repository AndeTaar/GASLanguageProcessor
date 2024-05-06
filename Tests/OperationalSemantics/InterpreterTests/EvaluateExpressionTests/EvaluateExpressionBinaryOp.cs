using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionBinaryOp
{
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpPlusNumber()
    {
        var visitor = new ToAstVisitor();
        var interpreter = new Interpreter();
        var inputStream = new AntlrInputStream(
            "2+6");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var expression = visitor.VisitExpression(parser.expression()) as Expression;
        var scope = new Scope(null, null);
        var result = interpreter.EvaluateExpression(expression, scope);


        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal((float) 8, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpPlusString()
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
        var visitor = new ToAstVisitor();
        var interpreter = new Interpreter();
        var inputStream = new AntlrInputStream(
            "7-5");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var expression = visitor.VisitExpression(parser.expression()) as Expression;
        var scope = new Scope(null, null);
        var result = interpreter.EvaluateExpression(expression, scope);


        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal((float) 2, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpMultiply()
    {
        var visitor = new ToAstVisitor();
        var interpreter = new Interpreter();
        var inputStream = new AntlrInputStream(
            "3*5");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var expression = visitor.VisitExpression(parser.expression()) as Expression;
        var scope = new Scope(null, null);
        var result = interpreter.EvaluateExpression(expression, scope);


        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal((float) 15, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpDivision()
    {
        var visitor = new ToAstVisitor();
        var interpreter = new Interpreter();
        var inputStream = new AntlrInputStream(
            "3/6");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var expression = visitor.VisitExpression(parser.expression()) as Expression;
        var scope = new Scope(null, null);
        var result = interpreter.EvaluateExpression(expression, scope);


        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal((float) 0.5, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpModulus()
    {
        var visitor = new ToAstVisitor();
        var interpreter = new Interpreter();
        var inputStream = new AntlrInputStream(
            "8%3");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var expression = visitor.VisitExpression(parser.expression()) as Expression;
        var scope = new Scope(null, null);
        var result = interpreter.EvaluateExpression(expression, scope);


        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal((float) 2, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpLessThan()
    {
        var visitor = new ToAstVisitor();
        var interpreter = new Interpreter();
        var inputStream = new AntlrInputStream(
            "3<6");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var expression = visitor.VisitExpression(parser.expression()) as Expression;
        var scope = new Scope(null, null);
        var result = interpreter.EvaluateExpression(expression, scope);


        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(true, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionBinaryOpGreaterThan()
    {
        var visitor = new ToAstVisitor();
        var interpreter = new Interpreter();
        var inputStream = new AntlrInputStream(
            "3>6");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var expression = visitor.VisitExpression(parser.expression()) as Expression;
        var scope = new Scope(null, null);
        var result = interpreter.EvaluateExpression(expression, scope);


        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(false, result);
    }
}