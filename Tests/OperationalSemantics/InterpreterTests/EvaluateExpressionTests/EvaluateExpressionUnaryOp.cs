using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionUnaryOp
{
    [Fact]
    public void PassEvaluateExpressionUnaryOpNegation()
    {
        var visitor = new ToAstVisitor();
        var interpreter = new Interpreter();
        var inputStream = new AntlrInputStream(
            "-20");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var expression = visitor.VisitExpression(parser.expression()) as Expression;
        var scope = new Scope(null, null);
        var result = interpreter.EvaluateExpression(expression, scope);

        Assert.NotNull(result);
        Assert.IsType<float>(result);
        Assert.Equal((float) -20, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionUnaryOpNot()
    {
        var visitor = new ToAstVisitor();
        var interpreter = new Interpreter();
        var inputStream = new AntlrInputStream(
            "!true");
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