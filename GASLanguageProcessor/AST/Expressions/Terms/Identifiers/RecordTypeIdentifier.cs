using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms.Identifiers;

public class RecordTypeIdentifier: Identifier
{
    public RecordTypeIdentifier(string name)
    {
        Name = name;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitRecordTypeIdentifier(this, envT);
    }
}
