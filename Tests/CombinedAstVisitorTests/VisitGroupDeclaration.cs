using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.CombinedAstVisitorTests;

public class VisitGroupDeclaration
{
    [Fact]
    public void VisitPassVisitGroupDeclaration()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "group g = Group(Point(10,10), { " +
            "num x = 1; " +
            "group mousEars = Group(Point(10,10), { " +
            "num y = 2; " +
            "});" +
            "group mousEyes = Group(Point(10,10), { " +
            "num y = 2; " +
            "});" +
            "});"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new TypeEnv());
        Assert.Empty(visitor.errors);
    }

    [Fact]
    public void VisitFailVisitGroupDeclaration()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "group g = Group(Point(10,10), { " +
            "   num x = 1; " +
            "   group mousEars = Group(Point(10,10), { " +
                    "num y = 2; " +
            "   });" +
            "   group mousEyes = Group(Point(10,10), { " +
            "       num y = 2; " +
            "   });" +
            "});" +
            "group g = Group(Point(10,10), { " +
            "   num x = 1; " +
            "   group mousEars = Group(Point(10,10), { " +
            "       num y = 2; " +
            "   });" +
            "   group mousEyes = Group(Point(10,10), { " +
            "       num y = 2; " +
            "   });" +
            "});"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new TypeEnv());
        Assert.NotEmpty(visitor.errors);
    }
}
