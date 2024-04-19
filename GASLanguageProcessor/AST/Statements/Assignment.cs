namespace GASLanguageProcessor.AST.Statements;

public class Assignment : Statement
{
    public string Identifier { get; protected set; }
    public AstNode Value { get; protected set; }

    public Assignment(string identifier, AstNode value)
    {
        Identifier = identifier;
        Value = value;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitAssignment(this);
    }
}