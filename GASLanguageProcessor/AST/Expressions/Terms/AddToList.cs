using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class AddToList : Term
{
    public AddToList(Expression value, Identifier listIdentifier)
    {
        Value = value;
        ListIdentifier = listIdentifier;
    }

    public Expression Value { get; protected set; }
    public Identifier ListIdentifier { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitAddToList(this, envT);
    }
}