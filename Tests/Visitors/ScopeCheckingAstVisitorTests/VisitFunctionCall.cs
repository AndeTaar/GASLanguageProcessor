using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.Visitors.ScopeCheckingAstVisitorTests;

public class VisitFunctionCall
{
    [Fact]
    public void VisitPassVisitFunctionCall()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "color color1 = Color(255, 255, 255, 1);" +
            "square square1 = Square(Point(10,10), 10, 10, Color(255, 255, 255, 1), Color(255, 255, 255, 1));" +
            "circle circle1 = Circle(Point(10,10), 10, 10, Color(255, 255, 255, 1), Color(255, 255, 255, 1));" +
            "line line1 = Line(Point(10,10), Point(20,20), 10, Color(255, 255, 255, 1));" +
            "text text1 = Text(\"Hello World\", Point(10, 10), \"Arial\", 12, Color(255, 255, 255, 1));"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitFunctionCall()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "color color1 = Color(255, 255, 1);"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.NotEmpty(visitor.errors);
    }
}
