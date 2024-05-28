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
            "canvas(100, 200, Colors(0, 0, 0, 1));" +
            "num x = 100 + 200;");

        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound)ast;
        var canvas = (Canvas)compound.Statement1;
        Assert.IsType<Compound>(compound.Statement2);
        var compound1 = (Compound)compound.Statement2;
        var declaration = (Declaration)compound1.Statement1;
        Assert.Null(compound1.Statement2);
        Assert.NotNull(declaration);
        Assert.IsType<Canvas>(canvas);
        Assert.NotNull(canvas);
        Assert.Equal("x", declaration.Identifier.Name);
        Assert.IsType<BinaryOp>(declaration.Expression);
        var binary = (BinaryOp)declaration.Expression;
        Assert.NotNull(binary);
        Assert.IsType<Num>(binary.Left);
        Assert.IsType<Num>(binary.Right);
        var left = (Num)binary.Left;
        var right = (Num)binary.Right;
        Assert.Equal("100", left.Value);
        Assert.Equal("200", right.Value);
    }
}