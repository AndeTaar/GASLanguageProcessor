namespace GASLanguageProcessor.AST.Statements;

public class If : Statement
{
    public AstNode Condition { get; protected set; }
    public AstNode Then { get; protected set; }
    public AstNode? Else { get; protected set; }

    public If(AstNode condition, AstNode then, AstNode @else)
    {
        Condition = condition;
        Then = then;
        Else = @else;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitIfStatement(this);
    }
}