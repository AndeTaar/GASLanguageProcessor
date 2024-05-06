using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Line : Term
{
    public Expression Intercept { get; protected set; }

    public Expression Gradient { get; protected set; }
    
    public Expression Stroke { get; protected set; }

    public Expression? Colour { get; protected set; }

    public Line(Expression intercept, Expression gradient, Expression stroke, Expression? colour)
    {
        Intercept = intercept;
        Gradient = gradient;
        Stroke = stroke;
        Colour = colour;
    }
    
    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitLine(this);
    }
}