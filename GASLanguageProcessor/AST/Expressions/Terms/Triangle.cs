using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Triangle : Term
{
    public Triangle(Expression trianglePeak, Expression triangleBase, Expression stroke, Expression? color,
        Expression? strokeColor)
    {
        TrianglePeak = trianglePeak;
        TriangleBase = triangleBase;
        Stroke = stroke;
        Color = color;
        StrokeColor = strokeColor;
    }

    public Expression TrianglePeak { get; protected set; }
    public Expression TriangleBase { get; protected set; }
    public Expression Stroke { get; protected set; }
    public Expression? Color { get; protected set; }
    public Expression? StrokeColor { get; protected set; }


    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitTriangle(this, envT);
    }
}