using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class Assignment : Statement
{
    public Identifier Identifier { get; protected set; }
    public Expression Value { get; protected set; }

    public Assignment(Identifier identifier, Expression value)
    {
        Identifier = identifier;
        Value = value;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitAssignment(this, scope);
    }
}
