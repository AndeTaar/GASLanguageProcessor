using GASLanguageProcessor.AST.Expressions.RecTerms;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions;

public class Record: RecTerm
{
    public RecType RecordType { get; protected set; }
    public List<Identifier> Identifiers { get; protected set; }
    public List<Expression> Expressions { get; protected set; }

    public Record(RecType recordType, List<Identifier> identifiers, List<Expression> expressions)
    {
        RecordType = recordType;
        Identifiers = identifiers;
        Expressions = expressions;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitRecord(this, envT);
    }
}
