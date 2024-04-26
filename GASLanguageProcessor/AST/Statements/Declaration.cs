using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST.Statements;

public class Declaration : Statement
{
    public Type Type { get; protected set; }
    public Identifier Identifier { get; protected set; }
    public Expression? Value { get; protected set; }

    public Declaration(Type type, Identifier identifier, Expression? value)
    {
        Type = type;
        Identifier = identifier;
        Value = value;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitDeclaration(this, scope);
    }
}
