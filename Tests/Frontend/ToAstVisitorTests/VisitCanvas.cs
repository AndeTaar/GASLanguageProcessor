using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using Xunit.Abstractions;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitCanvas
{
    [Fact]
    public void PassVisitCanvasWithColor()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));");
        Assert.NotNull(ast);
        Assert.IsType<Canvas>(ast);
        var canvas = (Canvas) ast;
        Assert.NotNull(canvas);
        Assert.IsType<Number>(canvas.Width);
        Assert.IsType<Number>(canvas.Height);
        var width = (Number) canvas.Width;
        var height = (Number) canvas.Height;
        Assert.Equal("250", width.Value);
        Assert.Equal("250", height.Value);
    }
}