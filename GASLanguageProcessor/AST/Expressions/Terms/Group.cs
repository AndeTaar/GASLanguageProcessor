using System.Collections.Generic;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Group: Term
{
    public Identifier Identifier { get; protected set; }
    public Statement Statements { get; protected set; }
    public Expression Point { get; protected set; }

    public Group(Identifier identifier, Expression point, Statement statements)
    {
        Identifier = identifier;
        Statements = statements;
        Point = point;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitGroup(this);
    }
}
