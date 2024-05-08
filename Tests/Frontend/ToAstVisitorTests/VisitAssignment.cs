using GASLanguageProcessor;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitAssignment
{
    [Fact]
    public void PassVisitAssignment()
    {
        var parser = SharedTesting.GetParser("x = 1;");
        var visitor = new ToAstVisitor();
        var context = parser.assignment();
        var result = visitor.VisitAssignment(context);
        
        Assert.NotNull(result);
        Assert.IsType<Assignment>(result);
    }
}
