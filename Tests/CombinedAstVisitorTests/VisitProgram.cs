using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

namespace Tests.CombinedAstVisitorTests;

public class VisitProgram
{
    [Fact]
    public void VisitPassVisitProgram()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));\n" +
            "number l1 = 10;\n" +
            "bool y1 = !true;\n" +
            "bool i1 = !!false;\n" +
            "if (true) { \n" +
            "   number x = 1; \n" +
            "}\n" +
            "else if (false) { \n" +
            "   number x = 1; \n" +
            "} else { \n" +
            "   number x = 1; \n" +
            "}\n" +
            "number i = 10 * 2 * 30 * 20;\n" +
            "number l = 10 * 2 * 30 * 20 - i;\n" +
            "group g = Group(Point(10,10), { \n" +
        "       number x = 1; \n" +
            "   group mousEars = Group(Point(10,10), { \n" +
            "       number y = 2; \n" +
            "   });\n" +
            "   group mousEyes = Group(Point(10,10), { \n" +
            "       number y = 2; \n" +
        "       });\n" +
            "});\n"
        );
        var visitor = new CombinedAstVisitor();
        ast.Accept(visitor, new Scope(null, null));
        Assert.Empty(visitor.errors);
    }
}