using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class StructAssignment: Statement
{
    public Identifier Identifier { get; protected set; }
    public Identifier StructIdentifier { get; protected set; }
    public List<Assignment>? Assignments { get; protected set; }

    public StructAssignment(Identifier identifier, Identifier structIdentifier, List<Assignment>? assignments)
    {
        Identifier = identifier;
        StructIdentifier = structIdentifier;
        Assignments = assignments;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitStructAssignment(this, envT);
    }
}
