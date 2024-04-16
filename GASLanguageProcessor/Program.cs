
using Antlr4.Runtime ;
using Antlr4.Runtime.Misc;

Main([" int i; i = 3+3*3; print true;"]);

static void Main(string[] args)
{
    var inputStream = CharStreams.fromString(args[0]);
    var lexer = new GASLexer(inputStream);

    var tokenStream = new CommonTokenStream(lexer);
    var parser = new GASParser(tokenStream);
    var parseTree = parser.program();

    AstNode ast = parseTree.Accept(new ToAstVisitor());
    Console.WriteLine(ast);
}