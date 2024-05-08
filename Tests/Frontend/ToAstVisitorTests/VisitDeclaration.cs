using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitDeclaration
{
    [Fact]
    public void PassVisitDeclaration()
    {
        var ast = SharedTesting.GetAst(
            "canvas(100, 200, Color(0, 0, 0, 1));" +
            "number x = 100 + 200;");

        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound)ast;
        var canvas = (Canvas)compound.Statement1;
        Assert.IsType<Declaration>(compound.Statement2);
        var declaration = (Declaration)compound.Statement2;
        Assert.IsType<Canvas>(canvas);
        Assert.NotNull(canvas);
        Assert.Equal("x", declaration.Identifier.Name);
        Assert.IsType<BinaryOp>(declaration.Expression);
        var binary = (BinaryOp)declaration.Expression;
        Assert.NotNull(binary);
        Assert.IsType<Number>(binary.Left);
        Assert.IsType<Number>(binary.Right);
        var left = (Number)binary.Left;
        var right = (Number)binary.Right;
        Assert.Equal("100", left.Value);
        Assert.Equal("200", right.Value);
    }
}