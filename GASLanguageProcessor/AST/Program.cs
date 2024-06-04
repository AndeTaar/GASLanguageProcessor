using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Program : AstNode
{
    public Statement Statements { get; set; }

    public Program(Statement statements)
    {
        Statements = statements;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitProgram(this, envT);
    }

}
