using Antlr4.Runtime;
using CARLLanguageProcessor;
using CARLLanguageProcessor.Frontend;
using CARLLanguageProcessor.TableType;

Main(new[] { "Frontend/test.carl" });


static void Main(string[] args)
{

    var fileContents = File.ReadAllText(args[0]);
    var inputStream = CharStreams.fromString(fileContents);
    var lexer = new CARLLexer(inputStream);

    var errorListener = new ParserErrorListener();
    var tokenStream = new CommonTokenStream(lexer);
    var parser = new CARLParser(tokenStream);
    parser.RemoveErrorListeners();
    parser.AddErrorListener(errorListener);
    var parseTree = parser.program();
    errorListener.StopIfErrors();
    var ast = parseTree.Accept(new ToAstVisitor());
    var interpreter = new Interpreter();
    var finalCValue = interpreter.EvaluateProgram(ast as CARLLanguageProcessor.AST.Expressions.Terms.Program);
    interpreter.errors.ForEach(Console.Error.WriteLine);
    if (interpreter.errors.Count > 0) return;
    Console.WriteLine(finalCValue);

}
