using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class FunctionCallTerm: Term
{
    public Identifier Identifier { get; protected set; }
    public List<Expression> Arguments { get; protected set; }

    public FunctionCallTerm(Identifier identifier, List<Expression> arguments)
    {
        Identifier = identifier;
        Arguments = arguments;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitFunctionCallTerm(this, envT);
    }
}
