using CARLLanguageProcessor.AST.Expressions.Terms;
using CARLLanguageProcessor.AST.Expressions.Terms.Identifiers;
using CARLLanguageProcessor.TableType;
using Type = CARLLanguageProcessor.AST.Expressions.Terms.Type;

namespace CARLLanguageProcessor.AST.Statements;

public class FunctionDeclaration : Statement
{
    public FunctionDeclaration(Identifier identifier, List<Parameter> parameters, Statement? statements,
        Type returnType)
    {
        Identifier = identifier;
        Parameters = parameters;
        Statements = statements;
        ReturnType = returnType;
    }

    public Type ReturnType { get; protected set; }
    public Identifier Identifier { get; protected set; }
    public List<Parameter> Parameters { get; protected set; }
    public Statement? Statements { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitFunctionDeclaration(this, envT);
    }
}
