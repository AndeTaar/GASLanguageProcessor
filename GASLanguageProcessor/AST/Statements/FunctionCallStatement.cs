using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

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