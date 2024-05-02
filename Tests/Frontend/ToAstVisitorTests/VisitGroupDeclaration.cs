using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions.Terms;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitGroupDeclaration
{
    [Fact]
    public void PassVisitGroupTermWithoutStatements()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "Group(Point(0,0),{});");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.groupTerm();
        var result = visitor.VisitGroupTerm(context);
        
        Assert.NotNull(result);
        Assert.IsType<Group>(result);
    }
    
    [Fact]
    public void PassVisitGroupTermWithStatements()
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(
            "Group(Point(0,0),{x = 1;});");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.groupTerm();
        var result = visitor.VisitGroupTerm(context);
        
        Assert.NotNull(result);
        Assert.IsType<Group>(result);
    }
}