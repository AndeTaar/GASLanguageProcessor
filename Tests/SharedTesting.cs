using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using CARLLanguageProcessor;
using CARLLanguageProcessor.AST;
using CARLLanguageProcessor.Frontend;
using CARLLanguageProcessor.TableType;

namespace Tests;

public static class SharedTesting
{
    public static CARLParser GetParser(string input)
    {
        var inputStream = new AntlrInputStream(input);
        var lexer = new CARLLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        return new CARLParser(tokenStream);
    }

    public static AstNode GetAst(string input)
    {
        var errorListener = new ParserErrorListener();
        var parser = GetParser(input);
        parser.RemoveErrorListeners();
        parser.AddErrorListener(errorListener);
        var context = parser.program();
        Assert.Empty(errorListener.Errors);
        return context.Accept(new ToAstVisitor());
    }

    public static (VarEnv, Store, TypeEnv, FuncEnv, List<string>) RunInterpreter(string input)
    {
        var ast = GetAst(input);
        var combinedAstVisitor = new CombinedAstVisitor();

        var envV = new VarEnv();
        var sto = new Store();
        var envT = new TypeEnv();
        var envF = new FuncEnv(sto, envV);

        ast.Accept(combinedAstVisitor, envT);
        var interpreter = new Interpreter();
        var item = interpreter.EvaluateStatement(ast as Statement, envV, envF, sto);
        var recordEvaluator = new RecordEvaluator();
        sto = recordEvaluator.EvaluateRecords(item.Item4);

        return (item.Item2, sto, envT, item.Item3, interpreter.errors);
    }

    public static ArrayList<string> GetSvgLines(string input)
    {
        var items = RunInterpreter(input);
        var recordEvaluator = new RecordEvaluator();
        var sto = recordEvaluator.EvaluateRecords(items.Item2);
        var svgGenerator = new SvgGenerator();
        var lines = svgGenerator.GenerateSvg(sto);
        lines.Add("</svg>");
        return lines;
    }
}
