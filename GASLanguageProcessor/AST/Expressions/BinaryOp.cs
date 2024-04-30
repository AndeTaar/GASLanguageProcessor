using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions;

public class BinaryOp : Expression
{
    public Expression Left { get; protected set; }
    public Expression Right { get; protected set; }
    public string Op { get; protected set; }
    public GasType? Type { get; set; }

    public BinaryOp(Expression left, string op, Expression right, GasType? type = null)
    {
        Left = left;
        Op = op;
        Right = right;
        Type = type;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitBinaryOp(this);
    }
}
