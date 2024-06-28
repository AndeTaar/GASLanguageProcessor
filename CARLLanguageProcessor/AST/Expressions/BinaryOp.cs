using CARLLanguageProcessor.AST.Types;
using CARLLanguageProcessor.TableType;

namespace CARLLanguageProcessor.AST.Expressions;

public class BinaryOp : Expression
{
    public BinaryOp(Expression left, string op, Expression right)
    {
        Left = left;
        Op = op;
        Right = right;
    }

    public Expression Left { get; protected set; }
    public Expression Right { get; protected set; }
    public string Op { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitBinaryOp(this, envT);
    }
}
