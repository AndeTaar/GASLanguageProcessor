using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitNotExpression
{
    [Fact]
    public void PassVisitNotExpression()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "bool x = !true;");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound)ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas)compound.Statement1;
        Assert.IsAssignableFrom<Compound>(compound.Statement2);
        var compound1 = (Compound)compound.Statement2;
        var declaration = (Declaration)compound1.Statement1;
        var eofNull = compound1.Statement2;
        Assert.Null(eofNull);
        Assert.NotNull(declaration);
        Assert.NotNull(canvas);
        Assert.Equal("x", declaration.Identifier.Name);
        Assert.IsAssignableFrom<UnaryOp>(declaration.Expression);
        var unaryOp = (UnaryOp)declaration.Expression;
        Assert.IsAssignableFrom<Boolean>(unaryOp.Expression);
        var boolean = (Boolean)unaryOp.Expression;
        Assert.NotNull(boolean);
    }
}