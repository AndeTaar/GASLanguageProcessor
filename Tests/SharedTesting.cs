using Antlr4.Runtime;
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

    public static Scope GetInterpretedScope(string input)
    {
        var ast = GetAst(input);
        var combinedAstVisitor = new CombinedAstVisitor();
        var scope = new Scope(null, null);
        ast.Accept(combinedAstVisitor, scope);
        var interpreter = new Interpreter();
        interpreter.EvaluateStatement(ast as Statement, scope);
        return scope;
    }

    public static AstNode FindFirstNodeType(AstNode ast, Type type)
    {
        if(type == typeof(Compound)) throw new Exception("Cannot search for Compound type, because of how the AST is structured.");
        if(ast.GetType() == type) return ast; // Kind of redundant, but here to cover all bases.
        if(ast.GetType() != typeof(Compound)) return null;

        Compound firstCompound = (Compound) ast;

        if(firstCompound.Statement1.GetType() == type) return firstCompound.Statement1;
        return FindFirstNodeType(firstCompound.Statement2, type);
    }
}
