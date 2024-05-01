using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;

namespace Tests.Frontend.ScopeCheckingAstVisitorTests;

public class VisitBinaryOp
{
    [Fact]
    public void PassVisitBinaryOp()
    {
        var visitor = new ScopeCheckingAstVisitor();
        var ast = SharedTesting.GenerateAst("canvas(200,200,Colour(255,255,255,1));bool a = 1==2;");
        var node = ast.Accept(visitor);

        Assert.True(node);
    }
}