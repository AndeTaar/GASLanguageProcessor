using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using GASLanguageProcessor;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.Frontend;
using GASLanguageProcessor.TableType;

namespace Tests;

public static class SharedTesting
{
    public static GASParser GetParser(string input)
    {
        var inputStream = new AntlrInputStream(input);
        var lexer = new GASLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        return new GASParser(tokenStream);
    }

    public static AstNode GetAst(string input)
    {
        ParserErrorListener errorListener = new ParserErrorListener();
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
        var envF = new FuncEnv(sto, envV, null);
        
        ast.Accept(combinedAstVisitor, envT);
        var interpreter = new Interpreter();
        interpreter.EvaluateStatement(ast as Statement, envV, envF, sto);
        
        return (envV, sto, envT, envF, interpreter.errors);
    }

    public static ArrayList<string> GetSvgLines(string input)
    {
        var env = RunInterpreter(input); 
        var svgGenerator = new SvgGenerator();
        var lines = svgGenerator.GenerateSvg(env.Item1, env.Item2);
        lines.Add("</svg>");
        return lines;
    }
}
