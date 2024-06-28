using CARLLanguageProcessor.AST;
using CARLLanguageProcessor.AST.Expressions;
using CARLLanguageProcessor.AST.Expressions.Terms;
using Boolean = CARLLanguageProcessor.AST.Expressions.Terms.Boolean;

namespace CARLLanguageProcessor;

public class ToAstVisitor : CARLBaseVisitor<AstNode>
{
    public override AstNode VisitProgram(CARLParser.ProgramContext context)
    {
        var expression = context.expression().Accept(this) as Expression;

        return new AST.Expressions.Terms.Program(expression) { LineNum = context.Start.Line };
    }

    public override AstNode VisitExpression(CARLParser.ExpressionContext context)
    {
        if (context.children.Count == 1) return base.VisitExpression(context);

        var equalityExpressions = context.equalityExpression().Select(ne => ne.Accept(this) as Expression).ToList();
        var expression = context.expression()?.Accept(this) as Expression;

        return new BinaryOp(equalityExpressions[0], context.GetChild(1).GetText(), expression ?? equalityExpressions[1])
            { LineNum = context.Start.Line };
    }

    public override AstNode VisitRelationExpression(CARLParser.RelationExpressionContext context)
    {
        if (context.children.Count == 1) return base.VisitRelationExpression(context);

        var binaryExpressions = context.binaryExpression().Select(ne => ne.Accept(this) as Expression).ToList();
        var relationExpression = context.relationExpression()?.Accept(this) as Expression;

        return new BinaryOp(binaryExpressions[0], context.GetChild(1).GetText(), relationExpression ?? binaryExpressions[1])
            { LineNum = context.Start.Line };
    }

    public override AstNode VisitBinaryExpression(CARLParser.BinaryExpressionContext context)
    {
        if (context.children.Count == 1) return base.VisitBinaryExpression(context);

        var multExpressions = context.multExpression().Select(ne => ne.Accept(this) as Expression).ToList();
        var binaryExpression = context.binaryExpression()?.Accept(this) as Expression;

        return new BinaryOp(multExpressions[0], context.GetChild(1).GetText(), binaryExpression ?? multExpressions[1])
            { LineNum = context.Start.Line };
    }

    public override AstNode VisitMultExpression(CARLParser.MultExpressionContext context)
    {
        if (context.children.Count == 1) return base.VisitMultExpression(context);

        var unaryExpressions = context.unaryExpression().Select(ne => ne.Accept(this) as Expression).ToList();
        var multExpression = context.multExpression()?.Accept(this) as Expression;

        return new BinaryOp(unaryExpressions[0], context.GetChild(1).GetText(), multExpression ?? unaryExpressions[1])
            { LineNum = context.Start.Line };
    }

    public override AstNode VisitUnaryExpression(CARLParser.UnaryExpressionContext context)
    {
        if (context.children.Count == 1) return base.VisitUnaryExpression(context);

        var expression = context.term().Accept(this) as Expression;

        return new UnaryOp(context.GetChild(0).GetText(), expression);
    }


    public override AstNode VisitTerm(CARLParser.TermContext context)
    {
        if (context.NUM() != null)
        {
            return new Num(context.NUM().GetText()) {LineNum = context.Start.Line};
        }
        else if (context.GetText() == "true" || context.GetText() == "false")
        {
            return new Boolean(context.GetText()) {LineNum = context.Start.Line};
        }
        else if (context.expression() != null)
        {
            return context.expression().Accept(this);
        }
        else
        {
            throw new NotSupportedException($"Term type not supported: {context.GetText()}");
        }
    }
}
