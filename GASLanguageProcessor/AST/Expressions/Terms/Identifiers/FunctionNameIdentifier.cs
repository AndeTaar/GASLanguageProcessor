using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms.Identifiers;

public class FunctionNameIdentifier: Identifier
{
    public FunctionNameIdentifier(string name)
    {
        Name = name;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitFunctionNameIdentifier(this, envT);
    }

}
