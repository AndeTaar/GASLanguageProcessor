using GASLanguageProcessor.AST.Statements;

namespace GASLanguageProcessor.AST.Terms;

public class Group: AstNode
{
    
    public Point Start { get; protected set; }
    public Compound Statements { get; protected set; }

    public Group(Point start, Compound statements)
    {
        Start = start;
        Statements = statements;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Statements.Accept(visitor, indent + "   ");
        Console.WriteLine(indent + this.GetType().Name);
        return this;
    }
}