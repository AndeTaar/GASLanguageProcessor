using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST.Statements;

public class Declaration : Statement
{
    public Declaration(Type type, Identifier identifier, Expression? expression)
    {
        Type = type;
        Identifier = identifier;
        Expression = expression;
    }

    public Type Type { get; set; }
    public Identifier Identifier { get; protected set; }
    public Expression? Expression { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitDeclaration(this, envT);
    }
}