using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.CombinedAstVisitorTests;

public class VisitNotExpression
{
    [Fact]
    public void VisitPassVisitNotExpression()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "if (!true) { " +
            "   num x = 1; " +
            "}" +
            "else if (!false) { " +
            "   num x = 1; " +
            "} else { " +
            "   num x = 1; " +
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
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "num x = 10;" +
            "bool y = !true;" +
            "bool i = !!false;"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }
}
