using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitDeclaration
{
    [Fact]
    public void PassVisitDeclaration()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "declaration test = Declaration (number x = 1;)");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.declaration();
        var result = visitor.VisitDeclaration(context);
        
        Assert.NotNull(result);
        Assert.IsType<Declaration>(result);
    }
}