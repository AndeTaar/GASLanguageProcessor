using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitReturnStatement
{
    [Fact]
    public void PassVisitReturnStatement()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "return 1;");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.returnStatement();
        var result = visitor.VisitReturnStatement(context);
        
        Assert.NotNull(result);
        Assert.IsType<Return>(result);
    }
}