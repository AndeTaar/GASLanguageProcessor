using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;
using Xunit.Abstractions;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitCanvas
{
    [Fact]
    public void PassVisitCanvas()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "canvas (250 * 2, 10 * 50, Colour(255, 255, 255, 1));");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.canvas();
        var result = visitor.VisitCanvas(context);
        
        Assert.NotNull(result);
        Assert.IsType<Canvas>(result);
    }
    
    [Fact]
    public void FailVisitCanvas()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "if (x > 4 || 2 < 9) {\n  x = 8;\n}");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.canvas();
        
        var exception = Assert.Throws<Exception>(() => visitor.VisitCanvas(context));
        Assert.Equal("Background colour is null", exception.Message);
    }
}