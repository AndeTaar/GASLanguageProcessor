using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionFunctionCall
{
    [Fact]
    public void PassEvaluateExpressionFunctionCall()
    {
        var typeCheckingVisitor = new TypeCheckingAstVisitor();
        var scopeCheckingVisitor = new ScopeCheckingAstVisitor();
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (125, 50, Color(255, 255, 255, 1)); " +
                                            "number Cool(number x) {return x+20*5;}" +
                                            "bool adfh = Cool(0);");
        ast.Accept(scopeCheckingVisitor);
        ast.Accept(typeCheckingVisitor);
        var result = interpreter.EvaluateStatement(ast as Statement, ast.Scope);
        Assert.NotNull(result);
    }
}