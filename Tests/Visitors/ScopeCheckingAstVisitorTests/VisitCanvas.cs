using GASLanguageProcessor;

namespace Tests.Visitors.ScopeCheckingAstVisitorTests;

public class VisitCanvas
{
    [Fact]
    public void VisitPassVisitCanvas()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.Empty(visitor.errors);
    }

}
