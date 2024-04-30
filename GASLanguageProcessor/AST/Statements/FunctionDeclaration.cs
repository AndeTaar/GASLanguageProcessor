using System.Collections.Generic;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.TableType;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST.Statements;

public class FunctionDeclaration: Statement
{
    public Type ReturnType { get; protected set; }
    public Identifier Identifier { get; protected set; }
    public List<Declaration> Declarations { get; protected set; }
    public Statement? Statements { get; protected set; }

    public FunctionDeclaration(Identifier identifier, List<Declaration> declarations, Statement? statements, Type returnType)
    {
        Identifier = identifier;
        Declarations = declarations;
        Statements = statements;
        ReturnType = returnType;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitFunctionDeclaration(this);
    }
}
