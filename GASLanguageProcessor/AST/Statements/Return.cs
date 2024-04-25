using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class Return: AstNode
{
    public AstNode Expression { get; protected set; }

    public Return(AstNode expression)
    {
        Expression = expression;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitReturn(this, scope);
    }
}