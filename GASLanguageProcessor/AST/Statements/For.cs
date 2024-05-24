using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class For : Statement
{
    public Statement Initializer { get; protected set; }
    public Expression Condition { get; protected set; }
    public Statement Incrementer { get; protected set; }
    public Statement Statements { get; protected set; }

    public For(Assignment assignment1, Expression condition, Assignment assignment2, Statement statements)
    {
        Initializer = assignment1;
        Condition = condition;
        Incrementer = assignment2;
        Statements = statements;
    }

    public For(Assignment assignment, Expression condition, Increment increment, Statement statements)
    {
        Initializer = assignment;
        Condition = condition;
        Incrementer = increment;
        Statements = statements;
    }

    public For(Declaration declaration, Expression condition, Assignment assignment, Statement statements)
    {
        Initializer = declaration;
        Condition = condition;
        Incrementer = assignment;
        Statements = statements;
    }

    public For(Declaration declaration, Expression condition, Increment increment, Statement statements)
    {
        Initializer = declaration;
        Condition = condition;
        Incrementer = increment;
        Statements = statements;
    }


    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitFor(this, scope);
    }
}
