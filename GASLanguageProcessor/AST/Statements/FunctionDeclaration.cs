using GASLanguageProcessor.AST.Expressions;

namespace GASLanguageProcessor.AST.Statements;

public class FunctionDeclaration: AstNode
{
    public AstNode ReturnType { get; protected set; }
    public Identifier Identifier { get; protected set; }
    public List<AstNode> Parameters { get; protected set; }
    public AstNode Body { get; protected set; }

    public FunctionDeclaration(Identifier identifier, List<AstNode> parameters, AstNode body, AstNode returnType)
    {
        Identifier = identifier;
        Parameters = parameters;
        Body = body;
        ReturnType = returnType;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitFunctionDeclaration(this);
    }
}