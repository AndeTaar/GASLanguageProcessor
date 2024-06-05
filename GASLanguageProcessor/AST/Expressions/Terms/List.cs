using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class List : Term
{
    public List(List<Expression> expressions, Type type)
    {
        Expressions = expressions;
        Type = type;
    }

    public Type Type { get; set; }

    public List<Expression> Expressions { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitList(this, envT);
    }
}