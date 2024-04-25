using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class Skip : Statement
{
    public Skip()
    {
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitSkip(this, scope);
    }
}