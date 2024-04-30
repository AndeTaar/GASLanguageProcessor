using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions.Terms;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitGroupDeclaration
{
    [Fact]
    public void PassVisitGroupDeclaration()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "group test = Group (number x = 1;)");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.groupDeclaration();
        var result = visitor.VisitGroupDeclaration(context);
        
        Assert.NotNull(result);
        Assert.IsType<Group>(result);
    }
}