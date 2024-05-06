using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using Type = System.Type;

namespace GASLanguageProcessor.AST.Statements;

public class CollectionDeclaration: Statement
{
    public Type Type { get; set; }
    public Identifier Identifier { get; protected set; }
    public Expression? Expression { get; protected set; }

    public CollectionDeclaration(Type type, Identifier identifier, Expression? expression)
    {
        Type = type;
        Identifier = identifier;
        Expression = expression;
    }


    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitCollectionDeclaration(this);
    }
}