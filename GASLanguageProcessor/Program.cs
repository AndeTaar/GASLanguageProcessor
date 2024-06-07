using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.Frontend;
using GASLanguageProcessor.TableType;

Main(new[] { "Frontend/test.gas" });


static void Main(string[] args)
{
    var outputDirectory = Path.Combine(Directory.GetCurrentDirectory().Split("bin")[0], "output");
    Directory.CreateDirectory(outputDirectory); // Create directory if it doesn't exist
    var FilePath = Path.Combine(outputDirectory, "CircleByPolygon.svg");

    var fileContents = File.ReadAllText(args[0]);

    var inputStream = CharStreams.fromString(fileContents);
    var lexer = new GASLexer(inputStream);

    var errorListener = new ParserErrorListener();

    var tokenStream = new CommonTokenStream(lexer);
    var parser = new GASParser(tokenStream);
    parser.RemoveErrorListeners();
    parser.AddErrorListener(errorListener);
    var parseTree = parser.program();
    errorListener.StopIfErrors();
    var ast = parseTree.Accept(new ToAstVisitor());
    var combinedAstVisitor = new CombinedAstVisitor();
    ast.Accept(combinedAstVisitor, new TypeEnv());
    combinedAstVisitor.errors.ForEach(Console.Error.WriteLine);
    if (combinedAstVisitor.errors.Count > 0) return;

    var interpreter = new Interpreter();
    var envV = new VarEnv();
    var sto = new Store();
    var envF = new FuncEnv(sto, envV);
    var recEnv = new RecEnv();
    var finalStore =
        interpreter.EvaluateProgram(ast as GASLanguageProcessor.AST.Expressions.Terms.Program, envV, envF, recEnv, sto);
    interpreter.errors.ForEach(Console.Error.WriteLine);
    if (interpreter.errors.Count > 0) return;
    var svgGenerator = new SvgGenerator(finalStore);
    var lines = svgGenerator.GenerateSvg(envV);
    lines.Add("</svg>");
    File.WriteAllLines(FilePath, lines);
    Console.WriteLine("SVG file generated at: " + FilePath);
}
