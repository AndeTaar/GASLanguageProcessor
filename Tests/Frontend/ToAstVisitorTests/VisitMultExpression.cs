using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitMultExpression
{
    [Fact]
    public void PassVisitMultExpression()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "a * b");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.multExpression();
        var result = visitor.VisitMultExpression(context);
        
        Assert.NotNull(result);
        Assert.IsType<BinaryOp>(result);
    }
}