using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;
using Tests;

public class VisitorTests
{
    [Fact]
    public void PassVisitFunctionDeclaration()
    {
        var ast = SharedTesting.GetAst(
            "canvas(100, 200, Color(0, 0, 0, 1));" +
            "void test(point x, point y, circle C){" +
            "    x = Point(10, 20);" +
            "    y = Point(30, 40);" +
            "    C = Circle(x, 10, 20);" +
            "}");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound)ast;
        var canvas = (Canvas)compound.Statement1;
        Assert.IsType<Compound>(compound.Statement2);
        var compound1 = (Compound)compound.Statement2;
        var functionDeclaration = (FunctionDeclaration)compound1.Statement1;
        Assert.IsType<Canvas>(canvas);
        Assert.NotNull(canvas);
        Assert.Equal("test", functionDeclaration.Identifier.Name);
        Assert.Equal(3, functionDeclaration.Parameters.Count);
        Assert.Equal("x", functionDeclaration.Parameters[0].Identifier.Name);
        Assert.Equal("y", functionDeclaration.Parameters[1].Identifier.Name);
        Assert.Equal("C", functionDeclaration.Parameters[2].Identifier.Name);
        Assert.IsAssignableFrom<Statement>(functionDeclaration.Statements);
        var compound2 = (Compound)functionDeclaration.Statements;
        var assignment1 = (Assignment)compound2.Statement1;
        var compound3 = (Compound)compound2.Statement2;
        var assignment2 = (Assignment)compound3.Statement1;
        var assignment3 = (Assignment)compound3.Statement2;
        Assert.Equal("x", assignment1.Identifier.Name);
        Assert.Equal("y", assignment2.Identifier.Name);
        Assert.Equal("C", assignment3.Identifier.Name);
    }
}