using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST.Statements;

public class RecordDefinition : Statement
{
    public RecordDefinition(Type recordType, List<Type> types, List<Identifier> identifiers)
    {
        RecordType = recordType;
        Types = types;
        Identifiers = identifiers;
    }

    public Type RecordType { get; protected set; }
    public List<Type> Types { get; protected set; }
    public List<Identifier> Identifiers { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitRecordDefinition(this, envT);
    }
}