using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitWhileStatement
{
    [Fact]
    public void PassVisitWhileStatement()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "while (true) {}");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.whileStatement();
        var result = visitor.VisitWhileStatement(context);
        
        Assert.NotNull(result);
        Assert.IsType<While>(result);
    }
}