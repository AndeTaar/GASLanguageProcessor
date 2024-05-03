using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;

namespace GASLanguageProcessor.AST.Statements;

public class FunctionCallStatement: Statement
{
    public Identifier Identifier { get; protected set; }
    public List<Expression> Arguments { get; protected set; }

    public FunctionCallStatement(Identifier identifier, List<Expression> arguments)
    {
        Identifier = identifier;
        Arguments = arguments;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitFunctionCallStatement(this);
    }
}
