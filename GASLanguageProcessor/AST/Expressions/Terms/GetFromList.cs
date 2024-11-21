using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class GetFromList : Term
{
    public GetFromList(Expression index, Identifier listIdentifier)
    {
        Index = index;
        ListIdentifier = listIdentifier;
    }

    public Expression Index { get; protected set; }
    public Identifier ListIdentifier { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitGetFromList(this, envT);
    }
}