using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class Assignment : Statement
{
    public Identifier Identifier { get; protected set; }
    public Expression Expression { get; protected set; }
    public string Operator { get; protected set; }

    public Assignment(Identifier identifier, Expression expression, string op)
    {
        Identifier = identifier;
        Expression = expression;
        Operator = op;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitAssignment(this, scope);
    }
}
