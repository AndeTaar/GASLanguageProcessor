using GASLanguageProcessor;

namespace Tests.Visitors.ScopeCheckingAstVisitorTests;

public class VisitForStatement
{
    [Fact]
    public void VisitPassVisitForStatement()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "for (number i = 0; i < 10; i = i + 1) {" +
            "number x = i;" +
            "}"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitForStatement()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "for (i = 0; i < 10; i = i + 1) { number i = 1; }"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.NotEmpty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitForStatement1()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "for (number i = 0; x < 10; i = i + 1) { number x; }"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.NotEmpty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitForStatement2()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "for (number i = 0; i < 10; i = x + 1) { x = 1; }"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.NotEmpty(visitor.errors);
    }
}
