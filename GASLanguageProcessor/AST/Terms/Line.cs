namespace GASLanguageProcessor.AST.Terms;

public class Line : AstNode
{
    public AstNode Start { get; protected set; }

    public AstNode End { get; protected set; }

    public AstNode Stroke { get; protected set; }

    public AstNode? Colour { get; protected set; }

    public AstNode? StrokeColour { get; protected set; }

    public Line(AstNode start, AstNode end, AstNode stroke, AstNode? colour, AstNode? strokeColour)
    {
        Start = start;
        End = end;
        Stroke = stroke;
        Colour = colour;
        StrokeColour = strokeColour;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitLine(this);
    }
}