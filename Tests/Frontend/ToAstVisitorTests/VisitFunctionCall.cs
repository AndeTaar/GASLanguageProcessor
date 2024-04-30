using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions.Terms;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitFunctionCall
{
    [Fact]
    public void PassVisitFunctionCall()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "print(\"Hello, World!\")");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.functionCall();
        var result = visitor.VisitFunctionCall(context);
        
        Assert.NotNull(result);
        Assert.IsType<FunctionCall>(result);
    }
}