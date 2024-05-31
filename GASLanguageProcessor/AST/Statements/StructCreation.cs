using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class StructCreation: Statement
{
    public Identifier Identifier { get; protected set; }
    public List<Declaration>? Fields { get; protected set; }

    public StructCreation(Identifier identifier, List<Declaration>? fields)
    {
        Identifier = identifier;
        Fields = fields;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitStructCreation(this, envT);
    }
}
