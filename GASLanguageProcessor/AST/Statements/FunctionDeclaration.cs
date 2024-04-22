using GASLanguageProcessor.AST.Expressions;

namespace GASLanguageProcessor.AST.Statements;

public class FunctionDeclaration: AstNode
{
    public AstNode ReturnType { get; protected set; }
    public Identifier Identifier { get; protected set; }
    public List<Declaration> Declarations { get; protected set; }
    public List<AstNode> Body { get; protected set; }

    public FunctionDeclaration(Identifier identifier, List<Declaration> declarations, List<AstNode> body, AstNode returnType)
    {
        Identifier = identifier;
        Declarations = declarations;
        Body = body;
        ReturnType = returnType;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitFunctionDeclaration(this);
    }
}