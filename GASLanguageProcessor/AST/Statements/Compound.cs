namespace GASLanguageProcessor.AST.Statements;

public class Compound : Statement
{
    public AstNode Statement1 { get; protected set; }
    public AstNode Statement2 { get; protected set; }

    public Compound(AstNode statement1, AstNode statement2)
    {
        Statement1 = statement1;
        Statement2 = statement2;
    }

    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Statement1.Accept(visitor, indent + "   ");
        Statement2.Accept(visitor, indent + "   ");
        Console.WriteLine(indent + this.GetType().Name);
        return this;
    }
}