using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitForStatement
{
    [Fact]
    public void PassVisitForStatement()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "for (number i = 0; i < 10; i = i + 1) { }");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<For>(compound.Statement2);
        var forStatement = (For) compound.Statement2;
        Assert.NotNull(forStatement);
        Assert.NotNull(canvas);
    }
}