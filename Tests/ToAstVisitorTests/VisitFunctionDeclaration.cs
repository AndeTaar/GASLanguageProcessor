using Xunit;
using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST;
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
        var canvas = (Canvas) compound.Statement1;
        Assert.IsType<FunctionDeclaration>(compound.Statement2);
        var functionDeclaration = (FunctionDeclaration)compound.Statement2;
        Assert.IsType<Canvas>(canvas);
        Assert.NotNull(canvas);
        Assert.Equal("test", functionDeclaration.Identifier.Name);
        Assert.Equal(3, functionDeclaration.Declarations.Count);
        Assert.Equal("x", functionDeclaration.Declarations[0].Identifier.Name);
        Assert.Equal("y", functionDeclaration.Declarations[1].Identifier.Name);
        Assert.Equal("C", functionDeclaration.Declarations[2].Identifier.Name);
        Assert.IsAssignableFrom<Statement>(functionDeclaration.Statements);
        var compound1 = (Compound) functionDeclaration.Statements;
        var assignment1 = (Assignment) compound1.Statement1;
        var compound2 = (Compound) compound1.Statement2;
        var assignment2 = (Assignment) compound2.Statement1;
        var assignment3 = (Assignment) compound2.Statement2;
        Assert.Equal("x", assignment1.Identifier.Name);
        Assert.Equal("y", assignment2.Identifier.Name);
        Assert.Equal("C", assignment3.Identifier.Name);
    }
}
