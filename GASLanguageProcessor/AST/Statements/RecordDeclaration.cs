using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.RecTerms;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST.Statements;

public class RecordDeclaration: Statement
{
    public RecordIdentifier RecordIdentifier { get; protected set; }

    public RecType RecType { get; protected set; }

    public RecordExpression RecordExpression { get; protected set; }

    public RecordDeclaration(RecordIdentifier recordIdentifier, RecType recType, RecordExpression recordExpression)
    {
        RecordIdentifier = recordIdentifier;
        RecType = recType;
        RecordExpression = recordExpression;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitRecordDeclaration(this, envT);
    }

}
