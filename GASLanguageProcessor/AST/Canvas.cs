using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor;

public class Canvas : Statement
{
    public Expression Width { get; protected set; }

    public Expression Height { get; protected set; }

    public Expression? BackgroundColour { get; protected set; }

    public Canvas(Expression width, Expression height, Expression? backgroundColour = null)
    {
        Width = width;
        Height = height;
        BackgroundColour = backgroundColour;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitCanvas(this);
    }
}
