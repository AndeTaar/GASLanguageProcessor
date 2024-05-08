using GASLanguageProcessor;

namespace Tests.Visitors.TypeCheckingAstVisitorTests;

public class VisitAssignment
{
    [Fact]
    public void VisitPassVisitAssignment()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number x = 10;" +
            "x = 10;"
        );
        var visitor = new TypeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitAssignment()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number x = 10;" +
            "x = true;"
        );
        var visitor = new TypeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.NotEmpty(visitor.errors);
    }
}
