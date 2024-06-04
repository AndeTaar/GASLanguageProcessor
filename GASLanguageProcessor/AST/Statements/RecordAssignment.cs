using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class RecordAssignment: Statement
{
    public RecordIdentifier RecordIdentifier { get; protected set; }
    public Record Record { get; protected set; }

    public RecordAssignment(RecordIdentifier recordIdentifier, Record record)
    {
        RecordIdentifier = recordIdentifier;
        Record = record;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitRecordAssignment(this, envT);
    }

}
