using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitBinaryExpression
{
    [Fact]
    public void PassVisitBinaryExpression()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "a + b");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.binaryExpression();
        var result = visitor.VisitBinaryExpression(context);
        
        Assert.NotNull(result);
        Assert.IsType<BinaryOp>(result);
    }
    
    [Fact]
    public void PassVisitBinaryExpressionWithOneChild()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "1");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.binaryExpression();
        var result = visitor.VisitBinaryExpression(context);
        
        Assert.NotNull(result);
        Assert.IsType<Number>(result);
    }
}