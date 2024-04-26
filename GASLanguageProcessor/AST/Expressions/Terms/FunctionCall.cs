using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Terms;

public class FunctionCall: Expression
{
    public Identifier Identifier { get; protected set; }
    public List<AstNode> Parameters { get; protected set; }

    public FunctionCall(Identifier identifier, List<AstNode> parameters)
    {
        Identifier = identifier;
        Parameters = parameters;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitFunctionCall(this, scope);
    }
}
