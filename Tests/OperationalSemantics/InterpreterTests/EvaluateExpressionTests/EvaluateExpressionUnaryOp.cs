using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionUnaryOp
{
    [Fact]
    public void PassEvaluateExpressionUnaryOpNegation()
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (-125, -50, Color(255, 255, 255, 1))") as Statement;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        var expected = new FinalCanvas(-125, -50, new FinalColor(255, 255, 255, 1));
        var result = interpreter.EvaluateStatement(ast, scope) as FinalCanvas;
        
        Assert.NotNull(result);
        Assert.IsType<FinalCanvas>(result);
        Assert.IsType<FinalCanvas>(expected);
        Assert.Equal(expected.Height, result.Height);
        Assert.Equal(expected.Width, result.Width);
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