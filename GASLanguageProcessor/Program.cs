using Antlr4.Runtime ;
using GASLanguageProcessor;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.TableType;

Main(new string[] {"Frontend/test.gas"});


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
    var globalScope = new Scope(null, null);
    var scopeCheckingVisitor = new ScopeCheckingAstVisitor();
    ast.Accept(scopeCheckingVisitor, globalScope);
    scopeCheckingVisitor.errors.ForEach(Console.Error.WriteLine);
    if(scopeCheckingVisitor.errors.Count > 0)
    {
        return;
    }
    ast.Accept(typeCheckingVisitor, globalScope);
    typeCheckingVisitor.errors.ForEach(Console.Error.WriteLine);
    if(typeCheckingVisitor.errors.Count > 0)
    {
        return;
    }
    Interpreter interpreter = new Interpreter();
    interpreter.EvaluateStatement(ast as Statement, globalScope);
    SvgGenerator svgGenerator = new SvgGenerator();
    svgGenerator.GenerateSvg(globalScope.vTable);
    Console.WriteLine(ast);
}
