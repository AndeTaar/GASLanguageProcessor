using System.Collections.Generic;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Group: Term
{
    public Expression Point { get; protected set; }
    public Statement? Statements { get; protected set; }

    public Group(Expression point, Statement? statements)
    {
        Point = point;
        Statements = statements;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitGroup(this, scope);
    }
}
