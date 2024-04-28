using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class FunctionCall: Term
{
    public Identifier Identifier { get; protected set; }
    public List<Expression> Arguments { get; protected set; }

    public FunctionCall(Identifier identifier, List<Expression> arguments)
    {
        Identifier = identifier;
        Arguments = arguments;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitFunctionCall(this, scope);
    }
}
