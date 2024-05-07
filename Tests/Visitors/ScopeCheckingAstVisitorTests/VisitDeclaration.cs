using GASLanguageProcessor;

namespace Tests.Visitors.ScopeCheckingAstVisitorTests;

public class VisitDeclaration
{
    [Fact]
    public void VisitPassVisitDeclaration()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number x;"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitDeclaration1()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number x;" +
            "number x;"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.NotEmpty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitDeclaration2()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number x;" +
            "for (x = 0; x < 10; x += 1) {}"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitDeclaration3()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "for (number x = 0; x < 10; x += 1) {}" +
            "for (number x = 0; x < 10; x += 1) {}"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitDeclaration3()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number x;" +
            "if (true) { number x; }"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.NotEmpty(visitor.errors);
    }

}
