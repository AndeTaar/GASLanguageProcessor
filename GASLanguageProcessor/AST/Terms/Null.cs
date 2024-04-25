using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Terms;

public class Null: AstNode
{
    public Null()
    {
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitNull(this, scope);
    }
}