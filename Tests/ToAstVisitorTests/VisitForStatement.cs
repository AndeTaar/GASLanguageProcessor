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
            "for (num i = 0; i < 10; i = i + 1) { }");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound)ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas)compound.Statement1;
        Assert.IsAssignableFrom<Compound>(compound.Statement2);
        var compound1 = (Compound)compound.Statement2;
        var forStatement = (For)compound1.Statement1;
        var eofNull = compound1.Statement2;
        Assert.Null(eofNull);
        Assert.NotNull(forStatement);
        Assert.NotNull(canvas);
    }
}