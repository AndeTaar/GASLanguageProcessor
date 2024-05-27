using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.CombinedAstVisitorTests;

public class VisitAssignment
{
    [Fact]
    public void VisitPassVisitAssignment()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "num x;" +
            "x = 1;"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new TypeEnv());
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitAssignment()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "x = 1;"
            );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new TypeEnv());
        Assert.NotEmpty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitAssignment2()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "num x;" +
            "x = y;"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.NotEmpty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitAssignment3()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "for ( num x = 0; x < 10; x += 1) {" +
            "}" +
            "x = 1;"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.NotEmpty(visitor.errors);
    }
}
