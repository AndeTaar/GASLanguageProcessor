namespace GASLanguageProcessor.AST.Terms;

public class Null: AstNode
{
    public Null()
    {
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitNull(this);
    }
}