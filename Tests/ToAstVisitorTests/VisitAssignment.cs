using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitAssignment
{
    [Fact]
    public void PassVisitAssignment()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255));" +
            "x = 1;"
        );
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound)ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas)compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var compound1 = (Compound)compound.Statement2;
        var assignment = (Assignment)compound1.Statement1;
        var eofNull = compound1.Statement2;
        Assert.Null(eofNull);
        Assert.NotNull(assignment);
        Assert.NotNull(canvas);
        Assert.Equal("x", assignment.Identifier.Name);
        Assert.IsAssignableFrom<Term>(assignment.Expression);
    }
}