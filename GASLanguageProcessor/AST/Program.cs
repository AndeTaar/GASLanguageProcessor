using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Program : AstNode
{
    public Compound Statements { get; set; }

    public Program(Compound statements)
    {
        Statements = statements;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitProgram(this, scope);
    }

}