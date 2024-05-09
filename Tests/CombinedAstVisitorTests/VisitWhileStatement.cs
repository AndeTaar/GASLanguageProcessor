using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.CombinedAstVisitorTests;

public class VisitWhileStatement
{
    [Fact]
    public void VisitPassVisitWhileStatement()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number x = 10;" +
            "while (x < 10) { " +
            "   x = x + 1; " +
            "}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitWhileStatement()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number x = 10;" +
            "while (y < 10) { " +
            "   x = x + 1; " +
            "}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.NotEmpty(visitor.errors);
    }
}
