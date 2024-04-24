namespace GASLanguageProcessor.AST.Statements;

public class While : Statement
{
    public AstNode Condition { get; protected set; }
    public AstNode Statements { get; protected set; }

    public While(AstNode condition, AstNode statements)
    {
        Condition = condition;
        Statements = statements;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitWhile(this);
    }
}