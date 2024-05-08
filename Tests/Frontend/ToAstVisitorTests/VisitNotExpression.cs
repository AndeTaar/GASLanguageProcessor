using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitNotExpression
{
    [Fact]
    public void PassVisitNotExpression()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "!a");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.unaryExpression();
        var result = visitor.VisitUnaryExpression(context);

        Assert.NotNull(result);
        Assert.IsType<UnaryOp>(result);
    }
}