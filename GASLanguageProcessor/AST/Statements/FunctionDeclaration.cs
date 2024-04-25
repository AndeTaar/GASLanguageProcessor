using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class FunctionDeclaration: AstNode
{
    public AstNode ReturnType { get; protected set; }
    public Identifier Identifier { get; protected set; }
    public List<Declaration> Declarations { get; protected set; }
    public AstNode? ReturnStatement { get; protected set; }
    public AstNode Statements { get; protected set; }

    public FunctionDeclaration(Identifier identifier, List<Declaration> declarations, AstNode statements, AstNode? returnStatement, AstNode returnType)
    {
        Identifier = identifier;
        Declarations = declarations;
        Statements = statements;
        ReturnStatement = returnStatement;
        ReturnType = returnType;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitFunctionDeclaration(this, scope);
    }
}