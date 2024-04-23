using Antlr4.Runtime ;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Terms;

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
    ast.Accept(typeCheckingVisitor);
    typeCheckingVisitor.typeErrors.ForEach(Console.Error.WriteLine);
    typeCheckingVisitor.scopeErrors.ForEach(Console.Error.WriteLine);

    
    Console.WriteLine(ast);
}
