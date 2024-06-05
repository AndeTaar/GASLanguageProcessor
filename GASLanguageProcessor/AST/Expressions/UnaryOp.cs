using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions;

public class UnaryOp : Expression
{
    public UnaryOp(string op, Expression expression)
    {
        Op = op;
        Expression = expression;
    }

    public Expression Expression { get; protected set; }
    public string Op { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitUnaryOp(this, envT);
    }
}