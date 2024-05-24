using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.CombinedAstVisitorTests;

public class VisitFunctionDeclaration
{
    [Fact]
    public void VisitPassVisitFunctionDeclaration()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "num test() {" +
            "   return 10;" +
            "}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitFunctionDeclaration()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "num test() {}" +
            "num test() {}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.NotEmpty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitFunctionDeclaration1()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "bool test() {" +
            "   num x = 10;" +
            "   return x > 0;" +
            "}" +
            "if(test()) {}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitFunctionDeclarationWithLists()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<num> test(list<num> nums) {" +
            "   return nums;" +
            "}" +
            "list<num> nus = test(List<num>{1, 2, 3, 4, 5});"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitPassVisitFunctionDeclaration2()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "num test(num x, point y, circle f) {" +
            "  x = 1;" +
            "  y = Point(1, 1);" +
            "  f = Circle(y, 10, 10, Color(255,255,255,1), Color(255,255,255,1));" +
            "  return x;" +
            "}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitFunctionDeclaration2()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "num test() {}" +
            "for (num x = 0; x < test(); x += 1) {}" +
            "for (num x = 0; x < test(); x += 1) {}" +
            "num test() {}"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.NotEmpty(visitor.errors);
    }

}
