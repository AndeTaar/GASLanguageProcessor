namespace GASLanguageProcessor.AST.Statements;

public class Else : Statement
{
    public AstNode Statements { get; protected set; }
    public If? If { get; protected set; }
    
    public Else(AstNode statements, If? @if)
    {
        Statements = statements;
        If = @if;
    }
    
    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitElseStatement(this);
    }
}