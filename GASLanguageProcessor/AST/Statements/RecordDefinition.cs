using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.TableType;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST.Statements;

public class RecordDefinition : Statement
{
    public RecordDefinition(Type recordType, List<Declaration> declarations, List<FunctionDeclaration> methods , List<ConstructorDeclaration> constructor)
    {
        RecordType = recordType;
        Declarations = declarations;
        FunctionDeclaration = methods;
        ConstructorDeclarations = constructor;
    }

    public Type RecordType { get; protected set; }
    public List<Declaration> Declarations { get; protected set; }
    public List<FunctionDeclaration> FunctionDeclaration { get; protected set; }
    public List<ConstructorDeclaration> ConstructorDeclarations { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitRecordDefinition(this, envT);
    }
}
