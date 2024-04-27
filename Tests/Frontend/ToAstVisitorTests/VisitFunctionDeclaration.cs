using Xunit;
using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Statements;

public class VisitorTests
{
    [Fact]
    public void PassVisitFunctionDeclaration()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "void test() {}");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.functionDeclaration();
        var result = visitor.VisitFunctionDeclaration(context);
      
        
        
        Assert.NotNull(result);
        Assert.IsType<FunctionDeclaration>(result);
    }
}