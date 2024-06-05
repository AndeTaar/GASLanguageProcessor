using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST.Expressions;

public class Record : Term
{
    public Record(Type recordType, List<Identifier> identifiers, List<Expression> expressions)
    {
        RecordType = recordType;
        Identifiers = identifiers;
        Expressions = expressions;
    }

    public Type RecordType { get; protected set; }
    public List<Identifier> Identifiers { get; protected set; }
    public List<Expression> Expressions { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitRecord(this, envT);
    }
}