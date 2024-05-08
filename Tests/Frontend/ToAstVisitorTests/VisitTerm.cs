using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitTerm
{
    [Fact]
    public void PassVisitTermNumber()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "number x = 1;"
            );
        float i = 10;
        float x = (i++);

        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var declaration = (Declaration) compound.Statement2;
        Assert.NotNull(declaration);
        Assert.NotNull(canvas);
        Assert.Equal("x", declaration.Identifier.Name);
        Assert.IsAssignableFrom<Term>(declaration.Expression);
        var term = (Term) declaration.Expression;
        Assert.IsType<Number>(term);
    }

    [Fact]
    public void PassVisitTermIdentifier()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "number x = y;"
        );

        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var declaration = (Declaration) compound.Statement2;
        Assert.NotNull(declaration);
        Assert.NotNull(canvas);
        Assert.Equal("x", declaration.Identifier.Name);
        Assert.IsAssignableFrom<Term>(declaration.Expression);
        var term = (Term) declaration.Expression;
        Assert.IsType<Identifier>(term);
    }

    [Fact]
    public void PassVisitTermFunctionCallStatement()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "test(50);"
        );

        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var functionCallStatement = (FunctionCallStatement) compound.Statement2;
        Assert.IsType<FunctionCallStatement>(functionCallStatement);
        Assert.NotNull(canvas);
        Assert.Equal("test", functionCallStatement.Identifier.Name);
    }

    [Fact]
    public void PassVisitTermFunctionCallTerm()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "number x = test(10, 40);"
        );

        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var declaration = (Declaration) compound.Statement2;
        Assert.NotNull(declaration);
        Assert.NotNull(canvas);
        Assert.Equal("x", declaration.Identifier.Name);
        Assert.IsAssignableFrom<Term>(declaration.Expression);
        var functionCallTerm = (FunctionCallTerm) declaration.Expression;
        Assert.IsType<FunctionCallTerm>(functionCallTerm);
    }

    [Fact]
    public void PassVisitTermString()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "string x = \"test\";");

        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var declaration = (Declaration) compound.Statement2;
        Assert.NotNull(declaration);
        Assert.NotNull(canvas);
        Assert.Equal("x", declaration.Identifier.Name);
        Assert.IsAssignableFrom<Term>(declaration.Expression);
        var term = (Term) declaration.Expression;
        Assert.IsType<String>(term);
    }

    [Fact]
    public void PassVisitTermExpression()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "if(a || b){}");

        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var ifStatement = (If) compound.Statement2;
        Assert.NotNull(ifStatement);
        Assert.NotNull(canvas);
        Assert.IsType<BinaryOp>(ifStatement.Condition);
        var binaryOp = (BinaryOp) ifStatement.Condition;
        Assert.NotNull(binaryOp);
    }

    [Fact]
    public void PassVisitTermBoolean()
    {
        var ast = SharedTesting.GetAst(
                "canvas(250, 250, Color(255, 255, 255, 1));" +
                "if(true){}");

        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var ifStatement = (If) compound.Statement2;
        Assert.NotNull(ifStatement);
        Assert.NotNull(canvas);
        Assert.IsType<Boolean>(ifStatement.Condition);
        var boolean = (Boolean) ifStatement.Condition;
        Assert.NotNull(boolean);
        Assert.Equal("true", boolean.Value);
    }

    [Fact]
    public void PassVisitTermNull()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "number x = null;"
        );

        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var declaration = (Declaration) compound.Statement2;
        Assert.NotNull(declaration);
        Assert.NotNull(canvas);
        Assert.Equal("x", declaration.Identifier.Name);
        Assert.IsAssignableFrom<Term>(declaration.Expression);
        var term = (Term) declaration.Expression;
        Assert.IsType<Null>(term);
    }
}