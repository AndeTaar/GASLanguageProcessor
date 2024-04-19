using GASLanguageProcessor.AST.Expressions;

namespace GASLanguageProcessor.AST.Statements;

public class FunctionDeclaration: AstNode
{
    public Identifier Identifier { get; protected set; }
    public AstNode Parameters { get; protected set; }
    public AstNode Body { get; protected set; }

    public FunctionDeclaration(Identifier identifier, AstNode parameters, AstNode body)
    {
        Identifier = identifier;
        Parameters = parameters;
        Body = body;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitFunctionDeclaration(this);
    }
}