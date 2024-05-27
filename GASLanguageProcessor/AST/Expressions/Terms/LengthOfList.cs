using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class LengthOfList : Term
{
    
    public Identifier ListIdentifier { get; protected set; }
    
    public LengthOfList(Identifier listIdentifier)
    {
        ListIdentifier = listIdentifier;
    }
    
    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitLengthOfList(this, envT);
    }
    
}