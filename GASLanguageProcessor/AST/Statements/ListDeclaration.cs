
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST.Statements;

public class ListDeclaration: Statement
{

    public ListDeclaration(Type type, Identifier identifier, Expression size)
    {
        Type = type;
        Identifier = identifier;
        Size = size;
    }

    public Type Type { get; protected set; }
    public Identifier Identifier { get; protected set; }
    public Expression Size { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitListDeclaration(this, envT);
    }
}