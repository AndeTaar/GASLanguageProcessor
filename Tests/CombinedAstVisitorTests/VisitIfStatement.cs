using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.CombinedAstVisitorTests;

public class VisitIfStatement
{
    [Fact]
    public void VisitPassVisitIfStatement()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "if (true) { " +
            "   num x = 1; " +
            "}" +
            "else if (false) { " +
            "   num x = 1; " +
            "} else { " +
            "   num x = 1; " +
            "}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new TypeEnv());
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitIfStatement()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "num x = 10;" +
            "if (true) { " +
            "   num x = 1; " +
            "}" +
            "else if (true) { " +
            "   num x = 1; " +
            "}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new TypeEnv());
        Assert.NotEmpty(visitor.errors);
    }
}
