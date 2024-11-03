using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.AST.Statements;
using Array = GASLanguageProcessor.AST.Expressions.Terms.Array;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using Expression = GASLanguageProcessor.AST.Expressions.Expression;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor;

public class ToAstVisitor : GASBaseVisitor<AstNode>
{
    public override AstNode VisitProgram(GASParser.ProgramContext context)
    {
        var lines = context.children
            .Select(line => line.Accept(this)).ToList();

        return new AST.Expressions.Terms.Program(ToCompound(lines));
    }

    public override AstNode VisitIfStatement(GASParser.IfStatementContext context)
    {
        var condition = context.expression().Accept(this) as Expression;

        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        var ifBody = ToCompound(statements);

        var @else = context.elseStatement()?.Accept(this) as Statement;

        return new If(condition, ifBody, @else) { LineNum = context.Start.Line };
    }

    public override AstNode VisitElseStatement(GASParser.ElseStatementContext context)
    {
        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        var elseBody = ToCompound(statements);

        var elseIf = context.ifStatement()?.Accept(this) as If;

        return elseIf ?? elseBody;
    }

    public override AstNode VisitWhileStatement(GASParser.WhileStatementContext context)
    {
        var condition = context.expression().Accept(this) as Expression;

        var statements = context.statement()
            .Select(s => s.Accept(this))
            .ToList();

        var whileBody = ToCompound(statements);

        return new While(condition, whileBody);
    }

    public override AstNode VisitForStatement(GASParser.ForStatementContext context)
    {
        var declaration = context.declaration()?.Accept(this) as Declaration;
        var assignments = context.assignment().Select(a => a.Accept(this) as Assignment).ToList();
        var increment = context.increment()?.Accept(this) as Increment;

        var condition = context.expression().Accept(this) as Expression;

        var allStatements = context.statement().Select(s => s.Accept(this)).ToList();
        var statements = ToCompound(allStatements);

        if (declaration != null)
        {
            if (increment != null) return new For(declaration, condition, increment, statements);

            return new For(declaration, condition, assignments[0], statements);
        }

        if (increment != null) return new For(assignments[0], condition, increment, statements);

        return new For(assignments[0], condition, assignments[1], statements) { LineNum = context.Start.Line };
    }

    public override AstNode VisitAssignment(GASParser.AssignmentContext context)
    {
        var IDENTIFIER = context.IDENTIFIER()?.Accept(this) as Identifier ??
                         context.ATTRIBUTEIDENTIFIER()?.Accept(this) as Identifier;
        if (IDENTIFIER == null) Console.WriteLine("error in assignment");
        var op = context.GetChild(1).GetText();
        var value = context.expression().Accept(this) as Expression;
        if (value != null && IDENTIFIER != null)
        {
            value.connectedIdentifier = IDENTIFIER.Name;
        }

        return new Assignment(IDENTIFIER, value, op) { LineNum = context.Start.Line };
    }

    public override AstNode VisitListAssignment(GASParser.ListAssignmentContext context)
    {
        var IDENTIFIER = context.IDENTIFIER().Accept(this) as Identifier;
        var index = context.expression()[0].Accept(this) as Expression;
        var value = context.expression()[1].Accept(this) as Expression;

        return new AddToArray(IDENTIFIER, index, value) { LineNum = context.Start.Line };
    }

    public override AstNode VisitArrayNewTerm(GASParser.ArrayNewTermContext context)
    {
        var type = context.type().Accept(this) as Type;
        var size = context.expression().Accept(this) as Expression;

        return new Array(type, size, new List<Expression>()) { LineNum = context.Start.Line };
    }

    public override AstNode VisitIncrement(GASParser.IncrementContext context)
    {
        var IDENTIFIER = context.IDENTIFIER().Accept(this) as Identifier;
        var op = context.GetChild(1).GetText();

        return new Increment(IDENTIFIER, op) { LineNum = context.Start.Line };
    }


    public override Group VisitGroupTerm(GASParser.GroupTermContext context)
    {
        var expression = context.expression().Accept(this) as Expression;

        var statements = ToCompound(context.statement()?.Select(c => c.Accept(this)).ToList());

        return new Group(expression, statements);
    }

    public override AstNode VisitDeclaration(GASParser.DeclarationContext context)
    {
        var type = context.type()?.Accept(this) as Type;

        if (type == null) type = context.collectionType().Accept(this) as Type;

        var IDENTIFIER = context.IDENTIFIER()?.Accept(this) as Identifier;

        var value = context.expression()?.Accept(this) as Expression;
        if (value != null && IDENTIFIER != null)
        {
            value.connectedIdentifier = IDENTIFIER.Name;
        }

        return new Declaration(type, IDENTIFIER, value) { LineNum = context.Start.Line };
    }

    public override AstNode VisitType(GASParser.TypeContext context)
    {
        return new Type(context.GetText()) { LineNum = context.Start.Line };
    }

    public override AstNode VisitCollectionType(GASParser.CollectionTypeContext context)
    {
        return new Type(context.GetText()) { LineNum = context.Start.Line };
    }

    public override AstNode VisitSimpleStatement(GASParser.SimpleStatementContext context)
    {
        return context.GetChild(0)?.Accept(this);
    }

    public override Expression VisitExpression(GASParser.ExpressionContext context)
    {
        var equalExpressions = context.equalityExpression().Select(mu => mu.Accept(this) as Expression).ToList();
        var left = equalExpressions[0] as Expression;
        var operatorIndex = 1;
        for (int i = 1; i < equalExpressions.Count; i++)
        {
            left = new BinaryOp(left, context.GetChild(operatorIndex).GetText(), equalExpressions[i]) {LineNum = context.Start.Line};
            operatorIndex += 2;
        }
        
        return left;
    }

    public override Expression VisitEqualityExpression(GASParser.EqualityExpressionContext context)
    {

        var left = context.GetChild(0).Accept(this) as Expression;

        var right = context.GetChild(2).Accept(this) as Expression;

        return new BinaryOp(left, context.GetChild(1).GetText(), right);
    }

    public override AstNode VisitRecDefinition(GASParser.RecDefinitionContext context)
    {
        var recordType = context.IDENTIFIER()[0].Accept(this) as Type;
        
        var types = context.allTypes().Select(t => t.Accept(this) as Type).ToList();
        var IDENTIFIERs = context.IDENTIFIER().Select(i => i.Accept(this) as Identifier).ToList();

        return new RecordDefinition(recordType, types, IDENTIFIERs) { LineNum = context.Start.Line };
    }

    public override AstNode VisitAllTypes(GASParser.AllTypesContext context)
    {
        return new Type(context.GetText()) { LineNum = context.Start.Line };
    }

    public override AstNode VisitReturnStatement(GASParser.ReturnStatementContext context)
    {
        var expression = context?.expression().Accept(this) as Expression;
        return new Return(expression);
    }
    
    public override FunctionCallStatement VisitFunctionCallStatement(GASParser.FunctionCallStatementContext context)
    {
        var IDENTIFIER = new Identifier(context.IDENTIFIER().GetText()) { LineNum = context.Start.Line };
        var arguments = context.expression().ToList().Select(expr => expr.Accept(this) as Expression).ToList();
        return new FunctionCallStatement(IDENTIFIER, arguments) {LineNum = context.Start.Line};
    }
    
    public override FunctionCallTerm VisitFunctionCallTerm(GASParser.FunctionCallTermContext context)
    {
        var IDENTIFIER = new Identifier(context.IDENTIFIER().GetText());
        var arguments = context.expression().ToList().Select(expr => expr.Accept(this) as Expression).ToList();
        return new FunctionCallTerm(IDENTIFIER, arguments) {LineNum = context.Start.Line};
    }

    public override AstNode VisitFunctionDeclaration(GASParser.FunctionDeclarationContext context)
    {
        var returnType = context.allTypes()[0].Accept(this) as Type;
        var IDENTIFIER = new Identifier(context.IDENTIFIER()[0].GetText()) { LineNum = context.Start.Line };

        var types = context.allTypes().Skip(1).ToList();
        var IDENTIFIERs = context.IDENTIFIER().Skip(1).ToList();

        var parameters = types.Zip(IDENTIFIERs, (typeNode, IDENTIFIERNode) =>
        {
            var type = typeNode.Accept(this) as Type;
            var IDENTIFIER = new Identifier(IDENTIFIERNode.GetText()) { LineNum = context.Start.Line };
            return new Parameter(type, IDENTIFIER);
        }).ToList();

        var statements = context.statement().Select(stmt => stmt.Accept(this)).ToList();
        var body = ToCompound(statements);
        return new FunctionDeclaration(IDENTIFIER, parameters, body, returnType) { LineNum = context.Start.Line };
    }

    public override Expression VisitRelationExpression(GASParser.RelationExpressionContext context)
    {
        var binaryExpressions = context.binaryExpression().Select(mu => mu.Accept(this) as Expression).ToList();
        var left = binaryExpressions[0] as Expression;
        var operatorIndex = 1;
        for (int i = 1; i < binaryExpressions.Count; i++)
        {
            left = new BinaryOp(left, context.GetChild(operatorIndex).GetText(), binaryExpressions[i]) {LineNum = context.Start.Line};
            operatorIndex += 2;
        }
        
        return left;
    }

    public override Expression VisitBinaryExpression(GASParser.BinaryExpressionContext context)
    {
        var multExpressions = context.multExpression().Select(mu => mu.Accept(this) as Expression).ToList();
        var left = multExpressions[0] as Expression;
        var operatorIndex = 1;
        for (int i = 1; i < multExpressions.Count; i++)
        {
            left = new BinaryOp(left, context.GetChild(operatorIndex).GetText(), multExpressions[i]) {LineNum = context.Start.Line};
            operatorIndex += 2;
        }
        
        return left;
    }

    public override Expression VisitTerm(GASParser.TermContext context)
    {
        if (context.NUM() != null)
        {
            return new Num(context.NUM().GetText()) {LineNum = context.Start.Line};
        }
        else if (context.functionCallTerm() != null)
        {
            return context.functionCallTerm().Accept(this) as FunctionCallTerm;
        }
        else if (context.ALLSTRINGS() != null)
        {
            return new String(context.ALLSTRINGS().GetText()) {LineNum = context.Start.Line};
        }
        if (context.ALLSTRINGS() != null)
            return new String(context.ALLSTRINGS().GetText()) { LineNum = context.Start.Line };
        if (context.expression() != null)
            return VisitExpression(context.expression());
        if (context.GetText() == "true" || context.GetText() == "false")
            return new Boolean(context.GetText()) { LineNum = context.Start.Line };
        if (context.GetText() == "null")
            return new Null();
        if (context.groupTerm() != null)
            return VisitGroupTerm(context.groupTerm());
        if (context.arrayTerm() != null)
            return VisitArrayTerm(context.arrayTerm());
        if (context.recordTerm() != null)
            return context.recordTerm().Accept(this) as Record;
        if (context.ATTRIBUTEIDENTIFIER() != null)
            return context.ATTRIBUTEIDENTIFIER().Accept(this) as Identifier;
        if(context.arrayAccessTerm() != null)
            return context.arrayAccessTerm().Accept(this) as GetFromArray;
        if(context.arrayNewTerm() != null)
            return context.arrayNewTerm().Accept(this) as Array;
        if(context.arraySizeTerm() != null)
            return context.arraySizeTerm().Accept(this) as SizeOfArray;
        if (context.IDENTIFIER() != null)
            return context.IDENTIFIER().Accept(this) as Identifier;
        throw new NotSupportedException($"Term type not supported: {context.GetText()}");
    }
    
    public override AstNode VisitArraySizeTerm(GASParser.ArraySizeTermContext context)
    {
        var IDENTIFIER = context.IDENTIFIER().Accept(this) as Identifier;
        return new SizeOfArray(IDENTIFIER) { LineNum = context.Start.Line };
    }

    public override AstNode VisitArrayAccessTerm(GASParser.ArrayAccessTermContext context)
    {
        var IDENTIFIER = context.IDENTIFIER().Accept(this) as Identifier;
        var index = context.expression().Accept(this) as Expression;
        return new GetFromArray(IDENTIFIER, index) { LineNum = context.Start.Line };
    }

    public override AstNode VisitRecordTerm(GASParser.RecordTermContext context)
    {
        var recordType = context.IDENTIFIER().Select(i => i.Accept(this) as Type).First();
        var IDENTIFIERs = context.IDENTIFIER().Select(i => i.Accept(this) as Identifier).ToList();
        var expressions = context.expression().Select(e => e.Accept(this) as Expression).ToList();

        expressions.ForEach(e => e.connectedIdentifier = IDENTIFIERs[expressions.IndexOf(e)].Name);

        return new Record(recordType, IDENTIFIERs, expressions) { LineNum = context.Start.Line };
    }

    public override Expression VisitArrayTerm(GASParser.ArrayTermContext context)
    {
        var type = context.type().Accept(this) as Type;
        var expressions = context.expression().Select(e => e.Accept(this) as Expression).ToList();
        var size = new Num(expressions.Count.ToString()) { LineNum = context.Start.Line };

        return new Array(type, size, expressions) { LineNum = context.Start.Line };
    }

    public override Expression VisitMultExpression(GASParser.MultExpressionContext context)
    {
        var unaryExpressions = context.unaryExpression().Select(mu => mu.Accept(this) as Expression).ToList();
        var left = unaryExpressions[0] as Expression;
        var operatorIndex = 1;
        for (int i = 1; i < unaryExpressions.Count; i++)
        {
            left = new BinaryOp(left, context.GetChild(operatorIndex).GetText(), unaryExpressions[i]) {LineNum = context.Start.Line};
            operatorIndex += 2;
        }
        
        return left;
    }

    public override AstNode VisitUnaryExpression(GASParser.UnaryExpressionContext context)
    {
        if (context.children.Count == 1) return base.VisitUnaryExpression(context);

        var expression = context.term().Accept(this) as Expression;

        return new UnaryOp(context.GetChild(0).GetText(), expression);
    }

    private static Statement ToCompound(List<AstNode> lines)
    {
        if (lines.Count == 0) return null!;

        if (lines.Count == 1) return lines[0] as Statement;

        if (lines[0] is Compound compound)
            return new Compound(compound.Statement1,
                new Compound(compound.Statement2, ToCompound(lines.Skip(1).ToList())));

        return new Compound(lines[0] as Statement, ToCompound(lines.Skip(1).ToList()));
    }
}
