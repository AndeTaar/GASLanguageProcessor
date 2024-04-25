﻿namespace GASLanguageProcessor.AST.Terms;

public class Square: AstNode
{
    public AstNode TopLeft { get; protected set; }
    public AstNode BottomRight { get; protected set; }

    public AstNode StrokeWidth { get; protected set; }

    public AstNode Colour { get; protected set; }

    public AstNode StrokeColour { get; protected set; }

    public Square(AstNode topLeft, AstNode bottomRight, AstNode strokeWidth, AstNode colour, AstNode strokeColour)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
        StrokeWidth = strokeWidth;
        Colour = colour;
        StrokeColour = strokeColour;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitSquare(this);
    }
}