using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions.Terms;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitType
{
    [Fact]
    public void PassVisitType()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "type test = Type (number x = 1;)");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.type();
        var result = visitor.VisitType(context);
        
        Assert.NotNull(result);
        Assert.IsType<Type>(result);
    }
}