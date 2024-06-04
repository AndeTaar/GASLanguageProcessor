using GASLanguageProcessor.AST.Expressions.RecTerms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class RecList: RecTerm
{
    public RecType RecType { get; protected set; }

    public List<RecordExpression> Expressions { get; protected set; }

    public RecList(RecType recTypeIdentifier, List<RecordExpression> expressions)
    {
        RecType = recTypeIdentifier;
        Expressions = expressions;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitRecList(this, envT);
    }
}
