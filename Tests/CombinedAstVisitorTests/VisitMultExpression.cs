using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.CombinedAstVisitorTests;

public class VisitMultExpression
{
    [Fact]
    public void VisitPassVisitMultExpression()
    {
        var ast = SharedTesting.GetAst("canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));");
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitMultExpression1()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number y = 10 * 2 * 30;" +
            "number i = 10 * 2 * 30 + y * 20;" +
            "number x = 10 * 2 * 30 + y * 20 - i;"
            );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }
}