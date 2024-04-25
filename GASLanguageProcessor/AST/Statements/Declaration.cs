using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class Declaration : Statement
{
    public AstNode Type { get; protected set; }
    public Identifier Identifier { get; protected set; }
    public AstNode? Value { get; protected set; }

    public Declaration(AstNode type, Identifier identifier, AstNode? value)
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