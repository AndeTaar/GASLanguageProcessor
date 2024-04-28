using Antlr4.Runtime ;
using GASLanguageProcessor;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.Frontend;
using GASLanguageProcessor.TableType;

Main(new string[] {"Frontend/test.gas"});


static void Main(string[] args)
{
    var fileContents = File.ReadAllText(args[0]);

    var inputStream = CharStreams.fromString(fileContents);
    var lexer = new GASLexer(inputStream);
    
    ParserErrorListener errorListener = new ParserErrorListener();
    
    var tokenStream = new CommonTokenStream(lexer);
    var parser = new GASParser(tokenStream);
    parser.RemoveErrorListeners();
    parser.AddErrorListener(errorListener);
    var parseTree = parser.program();
    errorListener.StopIfErrors();
    AstNode ast = parseTree.Accept(new ToAstVisitor());
    var typeCheckingVisitor = new TypeCheckingAstVisitor();
    var globalScope = new Scope(null, null);
    ast.Accept(typeCheckingVisitor, globalScope);
    typeCheckingVisitor.errors.ForEach(Console.Error.WriteLine);
    Console.WriteLine(ast);
}
