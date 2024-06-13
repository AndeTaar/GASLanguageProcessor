using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST.Statements;

public class RecordDefinition : Statement
{
    public RecordDefinition(Type recordType, Statement body)
    {
        RecordType = recordType;
        Body = body;
    }

    public Type RecordType { get; protected set; }
    public Statement Body { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitRecordDefinition(this, envT);
    }
}
