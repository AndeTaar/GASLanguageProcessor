using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class AddToArray : Statement
{
    public AddToArray(Identifier listIdentifier, Expression index, Expression value)
    {
        ListIdentifier = listIdentifier;
        Index = index;
        Value = value;
    }

    public Expression Value { get; protected set; }
    public Expression Index { get; protected set; }
    public Identifier ListIdentifier { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitAddToArray(this, envT);
    }
}