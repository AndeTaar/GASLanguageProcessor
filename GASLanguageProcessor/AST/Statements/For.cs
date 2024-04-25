using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class For : AstNode
{
    public AstNode Initializer { get; protected set; }
    public AstNode Condition { get; protected set; }
    public AstNode Increment { get; protected set; }
    public AstNode Body { get; protected set; }

    public For(AstNode initializer, AstNode condition, AstNode increment, AstNode body)
    {
        Initializer = initializer;
        Condition = condition;
        Increment = increment;
        Body = body;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitFor(this, scope);
    }
}