using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.Visitors.ScopeCheckingAstVisitorTests;

public class VisitRelationExpression
{
    [Fact]
    public void VisitPassVisitRelationExpression()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "if (10 == 10) { " +
            "   number x = 1; " +
            "}" +
            "else if (10 != 10) { " +
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
    public void VisitPassVisitRelationExpression1()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number x = 10;" +
            "bool y = x == 10;" +
            "bool i = x != 10;"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }
}
