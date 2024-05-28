using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.CombinedAstVisitorTests;

public class VisitDeclaration
{
    [Fact]
    public void VisitPassVisitDeclaration()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "num x;"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitDeclaration1()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "num x;" +
            "num x;"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.NotEmpty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitDeclaration2()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "num x;" +
            "for (x = 0; x < 10; x += 1) {}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitDeclaration3()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "for (num x = 0; x < 10; x += 1) {}" +
            "for (num x = 0; x < 10; x += 1) {}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitDeclaration3()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "num x;" +
            "if (true) { num x; }"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.NotEmpty(visitor.errors);
    }

}
