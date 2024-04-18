using Antlr4.Runtime ;
using GASLanguageProcessor;

 Main(["Frontend\\test.gas"]);

static void Main(string[] args)
{
    var fileContents = File.ReadAllText(args[0]);

    var inputStream = CharStreams.fromString(fileContents);
    var lexer = new GASLexer(inputStream);

    var tokenStream = new CommonTokenStream(lexer);
    var parser = new GASParser(tokenStream);
    var parseTree = parser.program();

    AstNode ast = parseTree.Accept(new ToAstVisitor());
    ast.Accept(new SimpleAstVisitor());
    Console.WriteLine(ast);
}