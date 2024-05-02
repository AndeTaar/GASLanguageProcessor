﻿using Antlr4.Runtime ;
using GASLanguageProcessor;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.Frontend;
using GASLanguageProcessor.TableType;

Main(new string[] {"Frontend/test.gas"});


static void Main(string[] args)
{
    var outputDirectory = Path.Combine(Directory.GetCurrentDirectory().Split("bin")[0], "output");
    Directory.CreateDirectory(outputDirectory); // Create directory if it doesn't exist
    string FilePath = Path.Combine(outputDirectory, "output.svg");

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
    var scopeCheckingVisitor = new ScopeCheckingAstVisitor();
    ast.Accept(scopeCheckingVisitor);
    scopeCheckingVisitor.errors.ForEach(Console.Error.WriteLine);
    if(scopeCheckingVisitor.errors.Count > 0)
    {
        return;
    }
    ast.Accept(typeCheckingVisitor);
    typeCheckingVisitor.errors.ForEach(Console.Error.WriteLine);
    if(typeCheckingVisitor.errors.Count > 0)
    {
        return;
    }
    Interpreter interpreter = new Interpreter();
    interpreter.EvaluateStatement(ast as Statement, scopeCheckingVisitor.scope);
    SvgGenerator svgGenerator = new SvgGenerator();
    var lines = svgGenerator.GenerateSvg(scopeCheckingVisitor.scope.vTable);
    lines.Add("</svg>");
    File.WriteAllLines(FilePath, lines);
    Console.WriteLine("SVG file generated at: " + FilePath);
}
