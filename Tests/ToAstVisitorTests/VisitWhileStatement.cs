using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitWhileStatement
{
    [Fact]
    public void PassVisitWhileStatement()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "while (true) {}");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound)ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas)compound.Statement1;
        Assert.IsAssignableFrom<Compound>(compound.Statement2);
        var compound1 = (Compound)compound.Statement2;
        var whileStatement = (While)compound1.Statement1;
        Assert.NotNull(whileStatement);
        Assert.NotNull(canvas);
    }
}