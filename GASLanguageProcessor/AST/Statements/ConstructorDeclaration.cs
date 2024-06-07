using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST.Statements;

public class ConstructorDeclaration: Statement
{
    public Type Type { get; protected set; }
    public List<Parameter> Parameters { get; protected set; }
    public Statement? Statements { get; protected set; }

    public ConstructorDeclaration(Type type, List<Parameter> parameters, Statement? statements)
    {
        Type = type;
        Parameters = parameters;
        Statements = statements;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitConstructorDeclaration(this, envT);
    }
}
