using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class Increment : Statement
{
    public Identifier Identifier { get; protected set; }
    public string Operator { get; protected set; }

    public Increment(Identifier identifier, string op)
    {
        Identifier = identifier;
        Operator = op;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitIncrement(this, envT);
    }
}