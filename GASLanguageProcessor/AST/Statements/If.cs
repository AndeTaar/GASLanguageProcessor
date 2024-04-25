using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class If : Statement
{
    public AstNode Condition { get; protected set; }
    public AstNode Statements { get; protected set; }
    public AstNode? Else { get; protected set; }

    public If(AstNode condition, AstNode statements, AstNode @else)
    {
        Condition = condition;
        Statements = statements;
        Else = @else;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitIfStatement(this, scope);
    }
}