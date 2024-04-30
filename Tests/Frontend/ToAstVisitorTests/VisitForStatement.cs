using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitForStatement
{
    [Fact]
    public void PassVisitForStatement()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "for (number i = 0; i < 10; i = i + 1) {}");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.forStatement();
        var result = visitor.VisitForStatement(context);
        
        Assert.NotNull(result);
        Assert.IsType<For>(result);
    }
}