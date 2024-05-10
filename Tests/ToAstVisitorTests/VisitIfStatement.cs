using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitIfStatement
{
    [Fact]
    public void PassVisitIfStatement()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "if (true) {}");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<If>(compound.Statement2);
        var ifStatement = (If) compound.Statement2;
        Assert.NotNull(ifStatement);
        Assert.NotNull(canvas);
    }

    [Fact]
    public void PassVisitIfStatementWithElse()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "if (true) {} else {}");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<If>(compound.Statement2);
        var ifStatement = (If) compound.Statement2;
        Assert.NotNull(ifStatement);
        Assert.NotNull(canvas);
    }

    [Fact]
    public void PassVisitIfStatementWithElseIf()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "if (true) {} else if (false) {}");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<If>(compound.Statement2);
        var ifStatement = (If) compound.Statement2;
        Assert.NotNull(ifStatement);
        Assert.NotNull(canvas);

        Assert.IsAssignableFrom<If>(ifStatement.Else);
        var elseIf = (If) ifStatement.Else;
        Assert.NotNull(elseIf);

        Assert.Null(elseIf.Else);
    }
}