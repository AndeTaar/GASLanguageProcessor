using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.Frontend;

namespace Tests.Frontend.ToAstVisitorTests.UnitTests;

public class VisitFunctionDeclaration
{
    [Fact]
    public void testFunctionDeclaration()
    {
        var fileContents = "void xFunc(num y){ z = 1 + y; }";

        var inputStream = CharStreams.fromString(fileContents);
        var lexer = new GASLexer(inputStream);
        ParserErrorListener errorListener = new ParserErrorListener();

        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        parser.RemoveErrorListeners();
        parser.AddErrorListener(errorListener);
        errorListener.StopIfErrors();
        Assert.NotNull(parser);
        
        var astVisitor = new ToAstVisitor();
        
        var functionDeclarationContext = parser.functionDeclaration();
        var functionDeclaration = (FunctionDeclaration) astVisitor.VisitFunctionDeclaration(functionDeclarationContext);
        
        Assert.NotNull(functionDeclaration);
        Assert.Equal("xFunc", functionDeclaration.Identifier.Name);
        Assert.Equal("num", functionDeclaration.Parameters[0].Type.Value);
        Assert.Equal("y", functionDeclaration.Parameters[0].Identifier.Name);
        Assert.Equal("void", functionDeclaration.ReturnType.Value);
        Assert.IsType<Assignment>(functionDeclaration.Statements);
        
        var assignment = functionDeclaration.Statements as Assignment;
        Assert.Equal("z", assignment.Identifier.Name);
        var assignmentVal = assignment.Expression;
        Assert.IsType<BinaryOp>(assignmentVal);
        var binaryOp = assignmentVal as BinaryOp;
        Assert.Equal("+", binaryOp.Op);
        Assert.IsType<Num>(binaryOp.Left);
        Assert.IsType<Identifier>(binaryOp.Right);
        var num = binaryOp.Left as Num;
        var identifier = binaryOp.Right as Identifier;
        Assert.Equal("1", num.Value);
        Assert.Equal("y", identifier.Name);
    }
}