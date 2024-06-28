using CARLLanguageProcessor.AST.Expressions;
using CARLLanguageProcessor.AST.Expressions.Terms.Identifiers;
using CARLLanguageProcessor.TableType;

namespace CARLLanguageProcessor.AST.Statements;

public class FunctionCallStatement : Statement
{
    public FunctionCallStatement(Identifier identifier, List<Expression> arguments)
    {
        Identifier = identifier;
        Arguments = arguments;
    }

    public Identifier Identifier { get; protected set; }
    public List<Expression> Arguments { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitFunctionCallStatement(this, envT);
    }
}
