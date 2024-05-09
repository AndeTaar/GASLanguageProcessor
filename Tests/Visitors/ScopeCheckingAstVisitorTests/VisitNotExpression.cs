using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.Visitors.ScopeCheckingAstVisitorTests;

public class VisitNotExpression
{
    [Fact]
    public void VisitPassVisitNotExpression()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "if (!true) { " +
            "   number x = 1; " +
            "}" +
            "else if (!false) { " +
            "   number x = 1; " +
            "} else { " +
            "   number x = 1; " +
            "}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitNotExpression1()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number x = 10;" +
            "bool y = !true;" +
            "bool i = !!1;"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }
}