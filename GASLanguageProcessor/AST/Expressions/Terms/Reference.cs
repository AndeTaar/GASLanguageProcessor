using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Reference: Term
{
    public Identifier Identifier { get; protected set; }

    public Reference(Identifier identifier)
    {
        Identifier = identifier;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitReference(this, envT);
    }
}