using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.Frontend;

namespace Tests.Frontend.ToAstVisitorTests.UnitTests;

public class VisitExpression
{
    [Fact]
    public void testExpression()
    {
        var fileContents = "x==y;";
    
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

        var expressionContext = parser.expression();
        var expression = (Expression) astVisitor.VisitExpression(expressionContext);
        
        Assert.NotNull(expression);
        Assert.IsType<BinaryOp>(expression);
        var binaryOp = expression as BinaryOp;
        Assert.Equal("==", binaryOp.Op);
        Assert.IsType<Identifier>(binaryOp.Left);
        Assert.IsType<Identifier>(binaryOp.Right);
        var left = binaryOp.Left as Identifier;
        var right = binaryOp.Right as Identifier;
        Assert.Equal("x", left.Name);
        Assert.Equal("y", right.Name);
    }


}