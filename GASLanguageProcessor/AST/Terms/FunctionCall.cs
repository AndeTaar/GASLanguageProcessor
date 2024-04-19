using GASLanguageProcessor.AST.Expressions;

namespace GASLanguageProcessor.AST.Terms;

public class FunctionCall: AstNode
{
    public AstNode Identifier { get; protected set; }
    public AstNode Parameters { get; protected set; }

    public FunctionCall(AstNode identifier, AstNode parameters)
    {
        Identifier = identifier;
        Parameters = parameters;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitFunctionCall(this);
    }
}