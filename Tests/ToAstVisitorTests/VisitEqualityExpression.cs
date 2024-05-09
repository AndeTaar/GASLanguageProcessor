using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitEqualityExpression
{
    [Fact]
    public void PassVisitEqualityExpression()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "bool c = true == false;");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var declaration = (Declaration) compound.Statement2;
        Assert.NotNull(declaration);
        Assert.NotNull(canvas);
        Assert.Equal("c", declaration.Identifier.Name);
        Assert.IsAssignableFrom<BinaryOp>(declaration.Expression);
        var binaryOp = (BinaryOp) declaration.Expression;
        Assert.IsAssignableFrom<Term>(binaryOp.Left);
        Assert.IsAssignableFrom<Term>(binaryOp.Right);
        var left = (Term) binaryOp.Left;
        var right = (Term) binaryOp.Right;
        Assert.NotNull(left);
        Assert.NotNull(right);
    }

    [Fact]
    public void PassVisitEqualityExpressionWithOneChild()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "bool c = true == false == true;");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var declaration = (Declaration) compound.Statement2;
        Assert.NotNull(declaration);
        Assert.NotNull(canvas);
        Assert.Equal("c", declaration.Identifier.Name);
        Assert.IsAssignableFrom<BinaryOp>(declaration.Expression);
        var binaryOp = (BinaryOp) declaration.Expression;
        Assert.IsAssignableFrom<Term>(binaryOp.Left);
        Assert.IsAssignableFrom<BinaryOp>(binaryOp.Right);
        var left = (Term) binaryOp.Left;
        var right = (BinaryOp) binaryOp.Right;
        Assert.NotNull(left);
        Assert.NotNull(right);
    }
}