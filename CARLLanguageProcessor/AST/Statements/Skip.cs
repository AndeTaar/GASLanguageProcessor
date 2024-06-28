using CARLLanguageProcessor.TableType;

namespace CARLLanguageProcessor.AST.Statements;

public class Skip : Statement
{
    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitSkip(this, envT);
    }
}
