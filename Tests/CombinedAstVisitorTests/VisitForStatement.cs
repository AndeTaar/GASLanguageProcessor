using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.CombinedAstVisitorTests;

public class VisitForStatement
{
    [Fact]
    public void VisitPassVisitForStatement()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "for (num i = 0; i < 10; i = i + 1) {" +
            "num x = i;" +
            "}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new TypeEnv());
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitForStatement1()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "for (num i = 0; i < 10; i = i + 1) {" +
            "num x = i;" +
            "}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new TypeEnv());
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitForStatement2()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<circle> circles;" +
            "color red = Color(255, 0, 0, 1);" +
            "color black = Color(0, 0, 0, 1);" +
            "for(num i = 0; i < 10; i = i + 1){" +
                "AddToList(Circle(Point(50, 10), 40+i, 10, red, black), circles);" +
                "circle circle1 = Circle(Point(50, 10), 40+i, 10, red, black);" +
                "}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new TypeEnv());
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitForStatement()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "for (i = 0; i < 10; i = i + 1) { num i = 1; }"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new TypeEnv());
        Assert.NotEmpty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitForStatement1()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "for (num i = 0; x < 10; i = i + 1) { num x; }"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new TypeEnv());
        Assert.NotEmpty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitForStatement2()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "for (num i = 0; i < 10; i = x + 1) { x = 1; }"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new TypeEnv());
        Assert.NotEmpty(visitor.errors);
    }
}
