using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.Frontend;

namespace Tests.Frontend.ToAstVisitorTests.UnitTests;

public class VisitAssignment
{

    [Fact]
    public void testAssignment()
    {
        var fileContents = "x = 1;";

        var inputStream = CharStreams.fromString(fileContents);
        var lexer = new GASLexer(inputStream);
        ParserErrorListener errorListener = new ParserErrorListener();

        var tokenStream = new CommonTokenStream(lexer);
        var parser = new GASParser(tokenStream);
        parser.RemoveErrorListeners();
        parser.AddErrorListener(errorListener);
        var parseTree = parser.program();
        Assert.NotNull(parseTree);
        errorListener.StopIfErrors();

        var astVisitor = new ToAstVisitor();

        var statementContext = (parseTree.GetChild(0) as GASParser.StatementContext);
        var simpleStatementContext = statementContext.GetChild(0) as GASParser.SimpleStatementContext;
        var assignmentContext = simpleStatementContext.GetChild(0) as GASParser.AssignmentContext;
        var assignment = (Assignment) astVisitor.VisitAssignment(assignmentContext);
        Assert.NotNull(assignment);
        Assert.Equal("x", assignment.Identifier.Name);
        Assert.IsType<Num>(assignment.Expression);
        var num = (Num)assignment.Expression;
        Assert.Equal("1", num.Value);
    }
}