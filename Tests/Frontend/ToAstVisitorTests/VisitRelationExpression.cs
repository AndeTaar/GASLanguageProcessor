using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitRelationExpression
{
    [Fact]
    public void PassVisitRelationExpression()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "1 < 2");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.relationExpression();
        var result = visitor.VisitRelationExpression(context);
        
        Assert.NotNull(result);
        Assert.IsType<BinaryOp>(result);
    }
    
    [Fact]
    public void PassVisitRelationExpressionWithOneChild()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "true");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.relationExpression();
        var result = visitor.VisitRelationExpression(context);
        
        Assert.NotNull(result);
        Assert.IsType<Boolean>(result);
    }
}