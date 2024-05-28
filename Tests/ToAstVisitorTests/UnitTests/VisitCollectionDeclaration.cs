using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.Frontend;

namespace Tests.Frontend.ToAstVisitorTests.UnitTests;

public class VisitCollectionDeclaration
{
    [Fact]
    public void testCollectionDeclaration()
    {
        var fileContents = "list<num> x = List<num>{ 13,25,39,41,55 }";

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
            
        var collectionDeclarationContext = parser.declaration();
        var collectionDeclaration = (Declaration) astVisitor.VisitDeclaration(collectionDeclarationContext);
        
        Assert.NotNull(collectionDeclaration);
        Assert.Equal("x", collectionDeclaration.Identifier.Name);
        Assert.Equal("list<num>", collectionDeclaration.Type.Value);
        Assert.IsType<List>(collectionDeclaration.Expression);
        var list = collectionDeclaration.Expression as List;
        Assert.Equal(5, list.Expressions.Count);
        Assert.All(list.Expressions, x => Assert.IsType<Num>(x));
        Assert.Collection(list.Expressions, 
            x => Assert.Equal("13", (x as Num)?.Value),
            x => Assert.Equal("25", (x as Num)?.Value),
            x => Assert.Equal("39", (x as Num)?.Value),
            x => Assert.Equal("41", (x as Num)?.Value),
            x => Assert.Equal("55", (x as Num)?.Value));
        Assert.Equal("num", list.Type.Value);
        
    }
}