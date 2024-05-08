using GASLanguageProcessor;

namespace Tests.Visitors.ScopeCheckingAstVisitorTests;

public class VisitProgram
{
[Fact]
    public void VisitPassVisitProgram()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number x = 10;" +
            "bool y = !true;" +
            "bool i = !!1;" +
            "if (true) { " +
            "   number x = 1; " +
            "}" +
            "else if (false) { " +
            "   number x = 1; " +
            "} else { " +
            "   number x = 1; " +
            "}" +
            "number y = 10 * 2 * 30;" +
            "number i = 10 * 2 * 30 + y * 20;" +
            "number x = 10 * 2 * 30 + y * 20 - i;" +
            "group g = Group(Point(10,10), { " +
            "number x = 1; " +
            "group mousEars = Group(Point(10,10), { " +
            "number y = 2; " +
            "});" +
            "group mousEyes = Group(Point(10,10), { " +
            "number y = 2; " +
            "});" +
            "});"
        );
        var visitor = new ScopeCheckingAstVisitor();
        ast.Accept(visitor);
        Assert.Empty(visitor.errors);
    }
}
