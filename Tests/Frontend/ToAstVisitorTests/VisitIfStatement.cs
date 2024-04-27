using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitIfStatement
{
    [Fact]
    public void PassVisitIfStatement()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "if (true) {}");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.ifStatement();
        var result = visitor.VisitIfStatement(context);
        
        Assert.NotNull(result);
        Assert.IsType<If>(result);
    }
}