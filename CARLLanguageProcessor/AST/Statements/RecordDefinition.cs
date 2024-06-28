using CARLLanguageProcessor.AST.Expressions.Terms.Identifiers;
using CARLLanguageProcessor.TableType;
using Type = CARLLanguageProcessor.AST.Expressions.Terms.Type;

namespace CARLLanguageProcessor.AST.Statements;

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
