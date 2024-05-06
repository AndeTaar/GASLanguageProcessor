using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class SegLine : Term
{
    public Expression Start { get; protected set; }

    public Expression End { get; protected set; }
    
    public Expression Stroke { get; protected set; }

    public Expression? Colour { get; protected set; }
    public SegLine(Expression start, Expression end, Expression stroke, Expression? colour)
    {
        Start = start;
        End = end;
        Stroke = stroke;
        Colour = colour;
    }
    
    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitSegLine(this);
    }
}
