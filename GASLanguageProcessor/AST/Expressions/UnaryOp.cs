using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions;

public class UnaryOp :  Expression
{
    public AstNode Expression { get; protected set; }
    public string Op { get; protected set; }

    public UnaryOp(string op, AstNode expression)
    {
        Op = op;
        Expression = expression;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitUnaryOp(this);
    }
}