using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Terms;

public class Type: AstNode
{
    public string Value { get; protected set; }

    public Type(string value)
    {
        Value = value;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitType(this, scope);
    }
}