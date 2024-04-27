using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class For : Statement
{
    public Declaration Declaration { get; protected set; }
    public Assignment Assignment { get; protected set; }
    public Expression Condition { get; protected set; }
    public Assignment Increment { get; protected set; }
    public Compound Body { get; protected set; }

    public For(Assignment assignment, Expression condition, Assignment increment, Compound body)
    {
        Assignment = assignment;
        Condition = condition;
        Increment = increment;
        Body = body;
    }

    public For(Declaration declaration, Expression condition, Assignment increment, Compound body)
    {
        Declaration = declaration;
        Condition = condition;
        Increment = increment;
        Body = body;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitFor(this, scope);
    }
}
