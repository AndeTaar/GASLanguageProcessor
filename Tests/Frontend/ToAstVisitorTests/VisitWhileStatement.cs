using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitWhileStatement
{
    [Fact]
    public void PassVisitWhileStatement()
    {
        var parser = SharedTesting.GetParser("while (true) {}");
        var visitor = new ToAstVisitor();
        var context = parser.whileStatement();
        var result = visitor.VisitWhileStatement(context);
        
        Assert.NotNull(result);
        Assert.IsType<While>(result);
    }
    
   // [Fact]
    public void FailVisitWhileStatement()
    {
        var ast = SharedTesting.GenerateAst
        (
            "canvas (250 * 2, 10 * 50, Colour(255, 255, 255, 1));" +
            "number x;" +
            "while(1+1 > 2) {" +
            "x = x + 1;" +
            "}"
        );

        var node = SharedTesting.FindFirstNodeType(ast, typeof(While)) as While;
        Assert.NotNull(node);
        Assert.NotNull(node.Statements);
        Assert.NotNull(node.Condition);

    }
}