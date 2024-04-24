using GASLanguageProcessor.AST.Expressions;

namespace GASLanguageProcessor.AST.Terms;

public class FunctionCall: AstNode
{
    public Identifier Identifier { get; protected set; }
    public List<AstNode> Parameters { get; protected set; }

    public FunctionCall(Identifier identifier, List<AstNode> parameters)
    {
        Identifier = identifier;
        Parameters = parameters;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitFunctionCall(this);
    }
}