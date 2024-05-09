using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions.Terms;
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
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var functionCallStatement = (FunctionCallStatement) compound.Statement2;
        Assert.NotNull(functionCallStatement);
        Assert.NotNull(canvas);
        Assert.Equal("print", functionCallStatement.Identifier.Name);
        Assert.NotEmpty(functionCallStatement.Arguments);
    }
}