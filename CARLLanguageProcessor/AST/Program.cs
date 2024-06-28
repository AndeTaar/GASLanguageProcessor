using CARLLanguageProcessor.TableType;

namespace CARLLanguageProcessor.AST.Expressions.Terms;

public class Program : AstNode
{
    public Program(Expression expression)
    {
        Expression = expression;
    }

    public Expression Expression { get; set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitProgram(this, envT);
    }
}
