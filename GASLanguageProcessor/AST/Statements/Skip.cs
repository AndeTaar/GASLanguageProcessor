namespace GASLanguageProcessor.AST.Statements;

public class Skip : Statement
{
    public Skip()
    {
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitSkip(this);
    }
}