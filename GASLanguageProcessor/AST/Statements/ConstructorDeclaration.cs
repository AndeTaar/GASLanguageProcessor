using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Statements;

public class ConstructorDeclaration: Statement
{
    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitConstructorDeclaration(this, envT);
    }
}
