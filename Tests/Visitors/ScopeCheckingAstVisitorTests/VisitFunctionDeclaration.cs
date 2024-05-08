using GASLanguageProcessor;

namespace Tests.Visitors.ScopeCheckingAstVisitorTests;

public class VisitFunctionDeclaration
{
    [Fact]
    public void VisitPassVisitFunctionDeclaration()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number test() {}"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitFunctionDeclaration()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number test() {}" +
            "number test() {}"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.NotEmpty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitFunctionDeclaration1()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "bool test() {}" +
            "if(test()) {}"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitFunctionDeclaration2()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number test(number x, point y, circle f) {" +
            "  x = 1;" +
            "  y = Point(1, 1);" +
            "  f = Circle(1, 1, 1);" +
            "}"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitFunctionDeclaration2()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number test() {}" +
            "for (number x = 0; x < test(); x += 1) {}" +
            "for (number x = 0; x < test(); x += 1) {}" +
            "number test() {}"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.NotEmpty(visitor.errors);
    }

}
