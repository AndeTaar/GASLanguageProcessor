using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class GetFromList : Term
{
    public Expression Index { get; protected set; }
    public Identifier ListIdentifier { get; protected set; }
    
    public GetFromList(Expression index, Identifier listIdentifier)
    {
        Index = index;
        ListIdentifier = listIdentifier;
    }
    
    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitGetFromList(this, scope);
    }
}