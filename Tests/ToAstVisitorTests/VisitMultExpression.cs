using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitMultExpression
{
    [Fact]
    public void PassVisitMultExpression()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "num x = 10 * 10;");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Declaration>(compound.Statement2);
        var declaration = (Declaration) compound.Statement2;
        Assert.NotNull(declaration);
        Assert.NotNull(canvas);
        Assert.Equal("x", declaration.Identifier.Name);
        Assert.IsAssignableFrom<BinaryOp>(declaration.Expression);
        var binaryOp = (BinaryOp) declaration.Expression;
        Assert.IsAssignableFrom<Term>(binaryOp.Left);
        Assert.IsAssignableFrom<Term>(binaryOp.Right);
        var left = (Term) binaryOp.Left;
        var right = (Term) binaryOp.Right;
        Assert.NotNull(left);
        Assert.NotNull(right);
    }
}