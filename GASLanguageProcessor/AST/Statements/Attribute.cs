using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;

namespace GASLanguageProcessor.AST.Statements;

public class Attribute : Statement
{
    public Identifier ObjectName { get; set; }
    public Identifier AttributeName { get; set; }
    
    public Attribute(Identifier objectName, Identifier attributeName)
    {
        ObjectName = objectName;
        AttributeName = attributeName;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitAttributeAccess(this);
    }
}