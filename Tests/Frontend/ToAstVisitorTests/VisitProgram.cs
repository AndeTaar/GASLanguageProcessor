using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitProgram
{
    [Fact]
    public void PassVisitProgram()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "canvas (250 * 2, 10 * 50, Colour(255,255,255,1));" +
            "number x = 0;");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.program();
        var result = visitor.VisitProgram(context);
        
        Assert.NotNull(result);
        Assert.IsType<Compound>(result);
    }
}