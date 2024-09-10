using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class StructTerm: Term
{
    public Identifier StructName { get; set; }
    public List<Assignment> Assignments { get; set; }

    public StructTerm(Identifier structName, List<Assignment> assignments)
    {
        StructName = structName;
        Assignments = assignments;
    }


    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitStructTerm(this, envT);
    }
}