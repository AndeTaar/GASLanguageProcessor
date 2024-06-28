using CARLLanguageProcessor.TableType;

namespace CARLLanguageProcessor.AST.Expressions.Terms;

public class Array : Term
{
    public Array(Type type, Expression size, List<Expression> expressions)
    {
        Expressions = expressions;
        Size = size;
        Type = type;
    }

    public Type Type { get; set; }
    public Expression Size { get; set; }
    public List<Expression> Expressions { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitArray(this, envT);
    }
}
