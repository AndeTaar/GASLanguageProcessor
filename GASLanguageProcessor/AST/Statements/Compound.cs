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

    public override AstNode Accept(IAstVisitor visitor)
    {
        var statement1 = Statement1?.Accept(visitor);
        var statement2 = Statement2?.Accept(visitor);
        Console.WriteLine(this.GetType().Name);
        return this;
    }
}