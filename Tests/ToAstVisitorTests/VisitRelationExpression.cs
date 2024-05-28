using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitRelationExpression
{
    [Fact]
    public void PassVisitRelationExpression()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "bool x = 1 < 2;");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Compound>(compound.Statement2);
        var compound1 = (Compound) compound.Statement2;
        var declaration = (Declaration) compound1.Statement1;
        var eofNull = compound1.Statement2;
        Assert.Null(eofNull);
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