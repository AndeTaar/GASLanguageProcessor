using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions;

public class UnaryOp :  Expression
{
    public Expression Expression { get; protected set; }
    public string Op { get; protected set; }

    public UnaryOp(string op, Expression expression)
    {
        Op = op;
        Expression = expression;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitUnaryOp(this);
    }
}