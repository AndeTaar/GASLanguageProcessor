using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms.Identifiers;

public class RecordIdentifier: Identifier
{
    public RecordIdentifier(string name)
    {
        Name = name;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitRecordIdentifier(this, envT);
    }
}
