using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class For : Statement
{
    public Declaration? Declaration { get; protected set; }
    public Assignment? Assignment { get; protected set; }
    public Expression Condition { get; protected set; }
    public Assignment Increment { get; protected set; }
    public Statement Statements { get; protected set; }

    public For(Assignment assignment, Expression condition, Assignment increment, Statement statements)
    {
        Assignment = assignment;
        Condition = condition;
        Increment = increment;
        Statements = statements;
    }

    public For(Declaration declaration, Expression condition, Assignment increment, Statement statements)
    {
        Declaration = declaration;
        Condition = condition;
        Increment = increment;
        Statements = statements;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitFor(this, scope);
    }
}
