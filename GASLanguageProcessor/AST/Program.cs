using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Program : AstNode
{
    public Program(Statement statements)
    {
        Statements = statements;
    }

    public Statement Statements { get; set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitProgram(this, envT);
    }
}