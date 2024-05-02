using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class List : Term
{
    public Type Type { get; set; }
    public List<Expression> Expressions { get; protected set; }

    public List(List<Expression> expressions, Type type)
    {
        Type = type;
        Expressions = expressions;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitList(this);
    }
}
