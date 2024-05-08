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
        var scopeVisitor = new ScopeCheckingAstVisitor();
        var typeVisitor = new TypeCheckingAstVisitor();
        ast.Accept(scopeVisitor);
        Assert.Empty(scopeVisitor.errors);
        ast.Accept(typeVisitor);
        Assert.Empty(typeVisitor.errors);
    }

    [Fact]
    public void VisitFailVisitAssignment()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number x = 10;" +
            "x = true;"
        );
        var scopeVisitor = new ScopeCheckingAstVisitor();
        var typeVisitor = new TypeCheckingAstVisitor();
        ast.Accept(scopeVisitor);
        Assert.Empty(scopeVisitor.errors);
        ast.Accept(typeVisitor);
        Assert.NotEmpty(typeVisitor.errors);
    }
}
