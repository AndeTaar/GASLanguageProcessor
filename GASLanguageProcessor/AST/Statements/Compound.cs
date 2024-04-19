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

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitCompound(this);
    }
}