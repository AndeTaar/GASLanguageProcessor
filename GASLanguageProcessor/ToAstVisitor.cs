﻿using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using Boolean = GASLanguageProcessor.AST.Expressions.Boolean;

namespace GASLanguageProcessor;

public class ToAstVisitor : GASBaseVisitor<AstNode> {
    public override AstNode VisitProgram ( GASParser.ProgramContext context )
    {
        var lines =  context.children
            .Select ( line => line.Accept(this)).ToList();

        return ToCompound(lines);
    }

    public override AstNode VisitCanvas(GASParser.CanvasContext context)
    {
        int.TryParse(context.GetChild(2).GetText(), out int width);
        int.TryParse(context.GetChild(4).GetText(), out int height);

        Colour backgroundColour = (context.GetChild(6)?.Accept(this) as Colour)!;

        return new Canvas(width, height, backgroundColour);
    }

    public override AstNode VisitAssignment(GASParser.AssignmentContext context)
    {
        var identifier = context.GetChild(0).GetText();

        if (identifier == null)
        {
            throw new Exception("Assignment context is null");
        }

        var value = context.GetChild(2).Accept(this) as AstNode;

        if (value == null)
        {
            throw new Exception("Expression is null");
        }
        return new Assignment(identifier, value);
    }

    public override AstNode VisitDeclaration(GASParser.DeclarationContext context)
    {
        var identifier = context.GetChild(1)?.GetText();

        if (identifier == null)
        {
            throw new Exception("Identifier is null");
        }

        var value = context.GetChild(3)?.Accept(this) as AstNode;

        return new Declaration(identifier, value);
    }

    public override AstNode VisitEqualityExpression(GASParser.EqualityExpressionContext context)
    {
        if (context.children.Count == 1)
        {
            return base.VisitEqualityExpression(context);
        }

        var left = context.GetChild(0).Accept(this);

        var right = context.GetChild(2).Accept(this);

        return new BinaryOp(left, context.GetChild(1).GetText(), right);
    }

    public override AstNode VisitRelationExpression(GASParser.RelationExpressionContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitRelationExpression(context);
        }

        var left = context.GetChild(0).Accept(this);

        var right = context.GetChild(2).Accept(this);

        return new BinaryOp(left, context.GetChild(1).GetText(), right);
    }

    public override AstNode VisitBinaryExpression(GASParser.BinaryExpressionContext context)
    {
        if (context.children.Count == 1)
        {
            return base.VisitBinaryExpression(context);
        }

        var left = context.GetChild(0).Accept(this);

        var right = context.GetChild(2).Accept(this);

        return new BinaryOp(left, context.GetChild(1).GetText(), right);
    }

    public override AstNode VisitMultExpression(GASParser.MultExpressionContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitMultExpression(context);
        }

        return new BinaryOp(context.GetChild(0).Accept(this), context.GetChild(1).GetText(), context.GetChild(2).Accept(this));
    }

    public override AstNode VisitNotExpression(GASParser.NotExpressionContext context)
    {
        if(context.children.Count == 1)
        {
            return base.VisitNotExpression(context);
        }

        var expression = context.GetChild(0).Accept(this);

        return new UnaryOp(context.GetChild(0).GetText(), expression);
    }

    private static AstNode ToCompound(List<AstNode> lines)
    {
        if(lines.Count == 1)
        {
            return lines[0];
        }

        if (lines[0] is Compound compound)
        {
            return new Compound(compound.Statement1,
                new Compound(compound.Statement2, ToCompound(lines.Skip(1).ToList())));
        }

        return new Compound(lines[0], ToCompound(lines.Skip(1).ToList()));
        }
}
