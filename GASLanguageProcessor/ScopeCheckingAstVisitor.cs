using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.TableType;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor;

public class ScopeCheckingAstVisitor: IAstVisitor<bool>
{
    public List<string> errors = new();

    public bool VisitBinaryOp(BinaryOp node, Scope scope)
    {
        var left = node.Left.Accept(this, scope);
        var right = node.Right.Accept(this, scope);
        return left && right;
    }

    public bool VisitGroup(Group node, Scope scope)
    {
        var groupScope = new Scope(scope, node);
        return node.Statements.Accept(this, groupScope);
    }

    public bool VisitNumber(Number node, Scope scope)
    {
        return true;
    }

    public bool VisitIfStatement(If node, Scope scope)
    {
        var condition = node.Condition.Accept(this, scope);
        var ifScope = new Scope(scope, node);
        var statements = node.Statements?.Accept(this, ifScope);
        var elseStatements = node.Else?.Accept(this, ifScope);
        return condition && (statements ?? true) && (elseStatements ?? true);
    }

    public bool VisitBoolean(Boolean node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool VisitIdentifier(Identifier node, Scope scope)
    {
        bool identifierIsInScope = false;
        try
        {
            identifierIsInScope = scope.vTable.LookUp(node.Name) != null;
        }
        catch (Exception e)
        {
            errors.Add("Variable name: " + node.Name + " not found");
        }
        return identifierIsInScope;
    }

    public bool VisitCompound(Compound node, Scope scope)
    {
        node.Statement1?.Accept(this, scope);
        node.Statement2?.Accept(this, scope);
        return true;
    }

    public bool VisitAssignment(Assignment node, Scope scope)
    {
        var identifier = node.Identifier.Name;
        var expression = node.Expression.Accept(this, scope);

        if (scope.vTable.LookUp(identifier) == null)
        {
            errors.Add("Line: " + node.LineNumber + " Variable name: " + identifier + " not found");
        }

        return expression;
    }

    public bool VisitDeclaration(Declaration node, Scope scope)
    {
        var identifier = node.Identifier.Name;
        var expression = node.Expression?.Accept(this, scope);
        scope.vTable.Bind(identifier, new Variable(identifier, node.Expression));
        return expression ?? true;
    }

    public bool VisitCanvas(Canvas node, Scope scope)
    {
        var width = node.Width.Accept(this, scope);
        var height = node.Height.Accept(this, scope);
        var backgroundColour = node.BackgroundColour.Accept(this, scope);
        return width && height && backgroundColour;
    }

    public bool VisitWhile(While node, Scope scope)
    {
        var condition = node.Condition.Accept(this, scope);
        var whileScope = new Scope(scope, node);
        var body = node.Statements.Accept(this, whileScope);
        return condition && body;
    }

    public bool VisitFor(For node, Scope scope)
    {
        var forScope = new Scope(scope, node);
        var declaration = node.Declaration?.Accept(this, forScope);
        var assignment = node.Assignment?.Accept(this, forScope);
        var increment = node.Increment.Accept(this, forScope);
        var condition = node.Condition.Accept(this, forScope);
        var body = node.Statements?.Accept(this, forScope);
        return condition && (declaration != null || assignment != null) && increment;
    }

    public bool VisitSkip(Skip node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool VisitUnaryOp(UnaryOp node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool VisitString(String s, Scope scope)
    {
        return true;
    }

    public bool VisitType(Type type, Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool VisitFunctionDeclaration(FunctionDeclaration functionDeclaration, Scope scope)
    {
        var identifier = functionDeclaration.Identifier.Name;
        var funcDeclScope = new Scope(scope, functionDeclaration);
        var parameters = functionDeclaration.Declarations.Select(decl =>
        {
            if (!decl.Accept(this, funcDeclScope))
            {
                errors.Add("Line: " + decl.LineNumber + " Variable name: " + decl.Identifier.Name + " not found");
            }
            return new Variable(decl.Identifier.Name, decl.Expression);
        }).ToList();
        var statements = functionDeclaration.Statements;
        scope.fTable.Bind(identifier, new Function(parameters, statements, funcDeclScope));
        return true;
    }

    public bool VisitFunctionCall(FunctionCall functionCall, Scope scope)
    {
        var identifier = functionCall.Identifier.Name;
        var function = scope.fTable.LookUp(identifier);

        if (function == null)
        {
            errors.Add("Line: " + functionCall.LineNumber + " Function name: " + identifier + " not found");
        }

        var parameters = functionCall.Arguments.Select(expression => expression.Accept(this, function?.Scope ?? scope)).ToList();
        if (parameters.Count != function?.Parameters.Count)
        {
            errors.Add("Line: " + functionCall.LineNumber + " Function name: " + identifier + " has wrong number of arguments");
        }

        return parameters.All(p => p);
    }

    public bool VisitReturn(Return @return, Scope scope)
    {
        return @return.Expression.Accept(this, scope);
    }

    public bool VisitNull(Null @null, Scope scope)
    {
        return true;
    }

    public bool VisitLine(Line line, Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool VisitText(Text text, Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool VisitCircle(Circle circle, Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool VisitRectangle(Rectangle rectangle, Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool VisitPoint(Point point, Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool VisitColour(Colour colour, Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool VisitSquare(Square square, Scope scope)
    {
        throw new NotImplementedException();
    }
}