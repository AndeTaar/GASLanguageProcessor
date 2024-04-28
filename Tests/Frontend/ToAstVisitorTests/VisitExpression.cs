using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitExpression
{
    [Fact]
    public void PassVisitExpression()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "(a || b)");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.expression();
        var result = visitor.VisitExpression(context);
        
        Assert.NotNull(result);
        Assert.IsType<BinaryOp>(result);
    }
    
    [Fact]
    public void PassVisitExpressionWithOneChild()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "true");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.expression();
        var result = visitor.VisitExpression(context);
        
        Assert.NotNull(result);
        Assert.IsType<Boolean>(result);
    }
}