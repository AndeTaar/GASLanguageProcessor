using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class Assignment : Statement
{
    public Assignment(Identifier identifier, Expression expression, string op)
    {
        Identifier = identifier;
        Expression = expression;
        Operator = op;
    }

    public Identifier Identifier { get; protected set; }
    public Expression Expression { get; protected set; }
    public string Operator { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitAssignment(this, envT);
    }
}