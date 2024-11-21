using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitFunctionCall
{
    [Fact]
    public void PassVisitFunctionCallStatement()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "print(\"Hello, World!\");");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound)ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas)compound.Statement1;
        Assert.IsAssignableFrom<Compound>(compound.Statement2);
        var compound1 = (Compound)compound.Statement2;
        var functionCallStatement = (FunctionCallStatement)compound1.Statement1;
        var eofNull = compound1.Statement2;
        Assert.Null(eofNull);
        Assert.NotNull(functionCallStatement);
        Assert.NotNull(canvas);
        Assert.Equal("print", functionCallStatement.Identifier.Name);
        Assert.NotEmpty(functionCallStatement.Arguments);
    }

    [Fact]
    public void PassVisitFunctionCallExpression()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "Color(255, 255, 255, 1);" +
            "Polygon(List<point>{Point(0, 0)}, 1, Color(255, 255, 255, 1), Color(255, 255, 255, 1));"
        );
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound)ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas)compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var compound1 = (Compound)compound.Statement2;
        Assert.IsAssignableFrom<FunctionCallStatement>(compound1.Statement1);
        var functionCallStatement = (FunctionCallStatement)compound1.Statement1;
        Assert.NotNull(functionCallStatement);
        Assert.NotNull(canvas);
        Assert.Equal("Color", functionCallStatement.Identifier.Name);
        Assert.NotEmpty(functionCallStatement.Arguments);
        Assert.IsAssignableFrom<Compound>(compound1.Statement2);
        var compound2 = (Compound)compound1.Statement2;
        var functionCallStatement1 = (FunctionCallStatement)compound2.Statement1;
        var eofNull = compound2.Statement2;
        Assert.Null(eofNull);
        Assert.NotNull(functionCallStatement1);
        Assert.Equal("Polygon", functionCallStatement1.Identifier.Name);
        Assert.NotEmpty(functionCallStatement1.Arguments);
    }
}