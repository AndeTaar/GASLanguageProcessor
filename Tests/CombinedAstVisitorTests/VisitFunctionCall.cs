using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.CombinedAstVisitorTests;

public class VisitFunctionCall
{
    [Fact]
    public void VisitPassVisitFunctionCall()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "colors color1 = Colors(255, 255, 255, 1);" +
            "square square1 = Square(Point(10,10), 10, 10, Colors(255, 255, 255, 1), Colors(255, 255, 255, 1),1);" +
            "circle circle1 = Circle(Point(10,10), 10, 10, Colors(255, 255, 255, 1), Colors(255, 255, 255, 1));" +
            "segLine line1 = SegLine(Point(10,10), Point(20,20), 10, Colors(255, 255, 255, 1));" +
            "line line2 = Line(100, 10, 10, Colors(255, 255, 255, 1));" +
            "text text1 = Text(\"Hello World\", Point(10, 10), \"Arial\", 12, 400, Colors(255, 255, 255, 1));"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitUseFunctionCallAsArgumentForOtherFunctionCall()
    {
        var ast = SharedTesting.GetAst(
            "colors color1 = Colors(255, 255, 255, 1);" +
            "square square1 = Square(Point(10,10), 10, 10, color1, Colors(255, 255, 255, 1),1);" +
            "circle circle1 = Circle(Point(10,10), 10, 10, color1, Colors(255, 255, 255, 1));" +
            "segLine line1 = SegLine(Point(10,10), Point(20,20), 10, Colors(255, 255, 255, 1));" +
            "line line2 = Line(100, 10, 10, color1);" +
            "text text1 = Text(\"Hello World\", Point(10, 10), \"Arial\", 12, 400, color1);" +
            "canvas (250 * 2, 10 * 50, color1);");
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitFunctionCall()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Colors(255, 255, 255, 1));" +
            "colors color1 = Colors(255, 255, 255);"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.NotEmpty(visitor.errors);
    }
}
