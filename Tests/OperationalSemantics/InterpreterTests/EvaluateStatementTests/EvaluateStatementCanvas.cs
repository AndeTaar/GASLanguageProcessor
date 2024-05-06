using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateStatementTests;

public class EvaluateStatementCanvas
{
 
    [Fact]
    public void PassEvaluateStatementCanvasWithColour()
    {
        var visitor = new ToAstVisitor();
        var interpreter = new Interpreter();
        var inputStream = new AntlrInputStream(
            "canvas (250, 250, Colour(255, 255, 255, 1));");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var statement = visitor.VisitCanvas(parser.canvas()) as Statement;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        var result = interpreter.EvaluateStatement(statement, scope);


        Assert.NotNull(result);
        Assert.IsType<FinalCanvas>(result);
    }
    
    [Fact]
    public void PassEvaluateStatementCanvasWithoutColour()
    {
        var visitor = new ToAstVisitor();
        var interpreter = new Interpreter();
        var inputStream = new AntlrInputStream(
            "canvas (250, 250);");
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        var statement = visitor.VisitCanvas(parser.canvas()) as Statement;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        var result = interpreter.EvaluateStatement(statement, scope);


        Assert.NotNull(result);
        Assert.IsType<FinalCanvas>(result);
    }
}