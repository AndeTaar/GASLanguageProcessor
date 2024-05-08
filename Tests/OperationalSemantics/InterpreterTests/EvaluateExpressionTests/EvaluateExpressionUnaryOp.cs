using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionUnaryOp
{
    [Fact]
    public void PassEvaluateExpressionUnaryOpNegation()
    {
        var typeCheckingVisitor = new TypeCheckingAstVisitor();
        var scopeCheckingVisitor = new ScopeCheckingAstVisitor();
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (-125, -50, Color(255, 255, 255, 1));number x = 20; number y = -x") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("x", new Variable("x", GasType.Number));
        scope.vTable.Bind("y", new Variable("y", GasType.Number));
        ast.Accept(scopeCheckingVisitor);
        ast.Accept(typeCheckingVisitor);
        var compound = ast.Statement2 as Compound;
        var result = interpreter.EvaluateStatement(compound.Statement2, scope);
        
        Assert.NotNull(result);
        Assert.Equal((float)-20, result);
    }
    
    [Fact]
    public void PassEvaluateExpressionUnaryOpNot()
    {
        var visitor = new ToAstVisitor();
        var interpreter = new Interpreter();
        var inputStream = new AntlrInputStream(
            "!true");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var expression = visitor.VisitExpression(parser.expression()) as Expression;
        var scope = new Scope(null, null);
        var result = interpreter.EvaluateExpression(expression, scope);

        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(false, result);
    }
}