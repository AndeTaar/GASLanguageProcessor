using Antlr4.Runtime ;
using GASLanguageProcessor;
using GASLanguageProcessor.TableType;

Main(["Frontend/test.gas"]);

static void Main(string[] args)
{
    var fileContents = File.ReadAllText(args[0]);

    var inputStream = CharStreams.fromString(fileContents);
    var lexer = new GASLexer(inputStream);

    var tokenStream = new CommonTokenStream(lexer);
    var parser = new GASParser(tokenStream);
    var parseTree = parser.program();

    AstNode ast = parseTree.Accept(new ToAstVisitor());
    var typeCheckingVisitor = new TypeCheckingAstVisitor();
    var globalScope = new Scope(null);
    ast.Accept(typeCheckingVisitor, globalScope);
    typeCheckingVisitor.errors.ForEach(Console.Error.WriteLine);
    Console.WriteLine(ast);
}
