using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Line : Term
{
    public Line(Expression intercept, Expression gradient, Expression stroke, Expression? color)
    {
        Intercept = intercept;
        Gradient = gradient;
        Stroke = stroke;
        Color = color;
    }

    public Expression Intercept { get; protected set; }

    public Expression Gradient { get; protected set; }

    public Expression Stroke { get; protected set; }

    public Expression? Color { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitLine(this, envT);
    }
}