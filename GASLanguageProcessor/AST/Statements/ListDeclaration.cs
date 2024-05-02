using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST.Statements;

public class ListDeclaration: Statement
{
    public Type Type { get; protected set; }
    public Identifier Identifier { get; protected set; }
    public List<Expression> Expressions { get; protected set; }

    public ListDeclaration(Type type, Identifier identifier, List<Expression> expressions)
    {
        Type = type;
        Identifier = identifier;
        Expressions = expressions;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitListDeclaration(this);
    }
}