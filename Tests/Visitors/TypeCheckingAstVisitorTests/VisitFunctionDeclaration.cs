using GASLanguageProcessor;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;

namespace Tests.Visitors.TypeCheckingAstVisitorTests;

public class VisitFunctionDeclaration
{
    [Fact]
    public void VisitPassSimpleFunctionDeclaration()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number numFunc(number x, bool y, string z)" +
            "{" +
            "   return x;" +
            "}" 
        );
        var scopeVisitor = new ScopeCheckingAstVisitor();
        var typeVisitor = new TypeCheckingAstVisitor();
        ast.Accept(scopeVisitor);
        Assert.Empty(scopeVisitor.errors);
        ast.Accept(typeVisitor);
        Assert.Empty(typeVisitor.errors);
        Assert.NotNull(scopeVisitor.scope.fTable.LookUp("numFunc"));
        Assert.Equal(GasType.Number, scopeVisitor.scope.fTable.LookUp("numFunc").ReturnType);
        List<Variable> parameters = scopeVisitor.scope.fTable.LookUp("numFunc").Parameters;
        Assert.Equal(GasType.Number, parameters[0].Type);
        Assert.Equal(GasType.Boolean, parameters[1].Type);
        Assert.Equal(GasType.String, parameters[2].Type);
        
    }
    
    [Fact]
    public void VisitFailFunctionDeclarationWithFunctionCall()
    {
        var ast = SharedTesting.GetAst(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "number numFunc(number x, bool y, string z)" +
            "{" +
            "   return x;" +
            "}" +
            "number failFunc() {" +
            "numFunc(true, 20, 10);" +
            "}" 
        );
        var scopeVisitor = new ScopeCheckingAstVisitor();
        var typeVisitor = new TypeCheckingAstVisitor();
        ast.Accept(scopeVisitor);
        Assert.Empty(scopeVisitor.errors);
        ast.Accept(typeVisitor);
        Assert.NotEmpty(typeVisitor.errors);
    }

}