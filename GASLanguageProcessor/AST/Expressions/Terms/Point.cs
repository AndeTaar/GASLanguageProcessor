using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Terms;

public class Point : Expression
{
    public AstNode X { get; protected set; }
    public AstNode Y { get; protected set; }

    public Point(AstNode x, AstNode y)
    {
        X = x;
        Y = y;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitPoint(this, scope);
    }
}
