using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Terms;

public class Group: Expression
{
    public Identifier Identifier { get; protected set; }
    public List<AstNode> Terms { get; protected set; }
    public AstNode Point { get; protected set; }

    public Group(Identifier identifier, AstNode point, List<AstNode> terms)
    {
        Identifier = identifier;
        Terms = terms;
        Point = point;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitGroup(this, scope);
    }
}
