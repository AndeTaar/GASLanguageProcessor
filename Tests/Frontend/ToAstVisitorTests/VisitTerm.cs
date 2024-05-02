using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitTerm
{
    private AstNode GetAstNode(string input)
    {
        var visitor = new ToAstVisitor();
        var inputStream = new AntlrInputStream(input);
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var context = parser.term();
        return visitor.VisitTerm(context);
    }
    
    
    [Fact]
    public void PassVisitTermNumber()
    {
        var result = GetAstNode("1");
        
        Assert.NotNull(result);
        Assert.IsType<Number>(result);
    }
    
    [Fact]
    public void PassVisitTermIdentifier()
    {
        var result = GetAstNode("a");
        
        Assert.NotNull(result);
        Assert.IsType<Identifier>(result);
    }
    
    [Fact]
    public void PassVisitTermFunctionCall()
    {
        var result = GetAstNode("func()");
        
        Assert.NotNull(result);
        Assert.IsType<FunctionCall>(result);
    }
    
    [Fact]
    public void PassVisitTermString()
    {
        var result = GetAstNode("\"test\"");
        
        Assert.NotNull(result);
        Assert.IsType<String>(result);
    }
    
    [Fact]
    public void PassVisitTermExpression()
    {
        var result = GetAstNode("(a || b)");
        
        Assert.NotNull(result);
        Assert.IsType<BinaryOp>(result);
    }
    
    [Fact]
    public void PassVisitTermBoolean()
    {
        var result = GetAstNode("true");
        
        Assert.NotNull(result);
        Assert.IsType<Boolean>(result);
    }
    
    [Fact]
    public void PassVisitTermNull()
    {
        var result = GetAstNode("null");
        
        Assert.NotNull(result);
        Assert.IsType<Null>(result);
    }
}