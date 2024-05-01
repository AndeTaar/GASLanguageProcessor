using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Statements;

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
    
    public static AstNode GenerateAst(string input)
    {
        var parser = GetParser(input);
        var context = parser.program();
        return context.Accept(new ToAstVisitor());
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
    
    public static String AddCanvas(string statement)
    {
        return "canvas(200,200,Colour(255,255,255,1));" + statement;
    }
}