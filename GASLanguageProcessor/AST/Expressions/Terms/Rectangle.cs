﻿using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class Rectangle: Expression
{
    public AstNode TopLeft { get; protected set; }

    public AstNode BottomRight { get; protected set; }

    public AstNode Stroke { get; protected set; }

    public AstNode? Colour { get; protected set; }

    public AstNode? StrokeColour { get; protected set; }

    public Rectangle(AstNode topLeft, AstNode bottomRight, AstNode stroke, AstNode? colour, AstNode? strokeColour)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
        Stroke = stroke;
        Colour = colour;
        StrokeColour = strokeColour;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitRectangle(this, scope);
    }
}