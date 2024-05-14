
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class List : Term
{
    public Type Type { get; set; }

    public List<Expression> Expressions { get; protected set; }

    public List(List<Expression> expressions, Type type)
    {
        Expressions = expressions;
        Type = type;
    }
 
    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitList(this, scope);
    }
}
