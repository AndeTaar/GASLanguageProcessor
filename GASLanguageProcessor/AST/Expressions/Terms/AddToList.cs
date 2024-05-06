namespace GASLanguageProcessor.AST.Expressions.Terms;

public class AddToList: Term
{
    public Term List { get; protected set; }

    public AddToList(Term list)
    {
        List = list;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitAddToList(this);
    }
}
