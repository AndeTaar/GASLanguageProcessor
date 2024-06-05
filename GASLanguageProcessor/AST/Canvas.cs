using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor;

public class Canvas : Statement
{
    public Canvas(Expression width, Expression height, Expression backgroundColor)
    {
        Width = width;
        Height = height;
        BackgroundColor = backgroundColor;
    }

    public Expression Width { get; protected set; }

    public Expression Height { get; protected set; }

    public Expression BackgroundColor { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitCanvas(this, envT);
    }
}