using GASLanguageProcessor;

namespace Tests.Frontend.ScopeCheckingAstVisitorTests;

public class VisitIfStatement
{
    [Fact]
    public void PassVisitIfStatement()
    {
        var visitor = new ScopeCheckingAstVisitor();
        var ast = SharedTesting.GenerateAst("canvas(200,200,Colour(255,255,255,1));if (1 == 1) {}");
        var node = ast.Accept(visitor);

        Assert.True(node);
    }
    
    [Fact]
    public void PassVisitIfStatementWithElse()
    {
        var visitor = new ScopeCheckingAstVisitor();
        var ast = SharedTesting.GenerateAst("canvas(200,200,Colour(255,255,255,1));if (1 == 1) {} else {}");
        var node = ast.Accept(visitor);

        Assert.True(node);
    }
    
    [Fact]
    public void PassVisitIfStatementWithSeperateScopedDeclarations()
    {
        var visitor = new ScopeCheckingAstVisitor();
        
        String input = "canvas(200,200,Colour(255,255,255,1));" +
                       "if (1 == 1) " +
                       "{" +
                           "number x = 8;" +
                       "}" +
                       "number x = 8;";
        
        var ast = SharedTesting.GenerateAst(input);
        var node = ast.Accept(visitor);

        Assert.True(node);
    }
    
    // [Fact]
    // public void FailVisitIfStatementWithSameScopedDeclarations()
    // {
    //     var visitor = new ScopeCheckingAstVisitor();
    //
    //     String input = "canvas(200,200,Colour(255,255,255,1));" +
    //                    "number x = 8;" +
    //                    "if (1 == 1) " +
    //                    "{" +
    //                         "number x = 8;" +
    //                    "}";
    //                    
    //     
    //     var ast = SharedTesting.GenerateAst(input);
    //     var node = ast.Accept(visitor);
    //
    //     Assert.True(visitor.errors.Count > 0); //This SHOULD be truthy
    // }
}