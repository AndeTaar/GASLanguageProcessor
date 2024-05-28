using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.CombinedAstVisitorTests;

public class VisitTerm
{
    [Fact]
    public void VisitPassVisitTerm()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "num x = 10;" +
            "num y = x * 10;"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitTerm1()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "num x = 10;" +
            "num y = x / 10;"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }
}
