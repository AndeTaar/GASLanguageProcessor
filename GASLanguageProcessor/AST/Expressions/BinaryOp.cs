using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions;

public class BinaryOp : Expression
{
    public Expression Left { get; protected set; }
    public Expression Right { get; protected set; }
    public string Op { get; protected set; }

    public BinaryOp(Expression left, string op, Expression right)
    {
        Left = left;
        Op = op;
        Right = right;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitBinaryOp(this, scope);
    }
}