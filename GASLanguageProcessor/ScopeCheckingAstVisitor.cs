using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor;

public class ScopeCheckingAstVisitor: IAstVisitor<bool>
{
    public Scope scope { get; set; } = new Scope(null, null);

    public List<string> errors = new();

    public bool VisitBinaryOp(BinaryOp node)
    {
        node.Scope = scope;
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);
        return left && right;
    }

    public bool VisitGroup(Group node)
    {
        node.Scope = scope;
        scope = scope.EnterScope(node);
        node.Point.Accept(this);
        node.Statements.Accept(this);
        scope = scope.ExitScope();
        return true;
    }

    public bool VisitList(List node)
    {
        throw new NotImplementedException();
    }

    public bool VisitListDeclaration(ListDeclaration node)
    {
        node.Scope = scope;
        var type = node.Type is GasType;
        scope = scope.EnterScope(node);
        var elements = node.Expressions.Select(e => e.Accept(this)).ToList();
        scope = scope.ExitScope();
        return type && elements.All(e => e);
    }

    public bool VisitNumber(Number node)
    {
        node.Scope = scope;
        return true;
    }

    public bool VisitIfStatement(If node)
    {
        node.Scope = scope;
        var condition = node.Condition.Accept(this);
        scope = scope.EnterScope(node);
        var statements = node.Statements?.Accept(this);
        var elseStatements = node.Else?.Accept(this);
        scope = scope.ExitScope();
        return condition && (statements ?? true) && (elseStatements ?? true);
    }

    public bool VisitBoolean(Boolean node)
    {
        node.Scope = scope;
        return true;
    }

    public bool VisitIdentifier(Identifier node)
    {
        node.Scope = scope;
        bool identifierIsInScope = false;
        try
        {
            identifierIsInScope = scope.vTable.LookUp(node.Name) != null;
        }
        catch (Exception e)
        {
            errors.Add("Line: " + node.LineNumber + " Variable name: " + node.Name + " not found");
        }
        return identifierIsInScope;
    }

    public bool VisitCompound(Compound node)
    {
        node.Scope = scope;
        node.Statement1?.Accept(this);
        node.Statement2?.Accept(this);
        return true;
    }

    public bool VisitAssignment(Assignment node)
    {
        node.Scope = scope;
        var identifier = node.Identifier.Name;
        var expression = node.Expression.Accept(this);

        if (scope.vTable.LookUp(identifier) == null)
        {
            errors.Add("Line: " + node.LineNumber + " Variable name: " + identifier + " not found");
        }

        return expression;
    }

    public bool VisitDeclaration(Declaration node)
    {
        node.Scope = scope;
        var identifier = node.Identifier.Name;
        var expression = node.Expression?.Accept(this);
        scope.vTable.Bind(identifier, new Variable(identifier, node.Expression));
        return expression ?? true;
    }

    public bool VisitCanvas(Canvas node)
    {
        node.Scope = scope;
        var width = node.Width.Accept(this);
        var height = node.Height.Accept(this);
        var backgroundColour = node.BackgroundColour?.Accept(this);
        scope.vTable.Bind("canvas", new Variable("canvas", node));
        return width && height && (backgroundColour ?? true);
    }

    public bool VisitWhile(While node)
    {
        node.Scope = scope;
        var condition = node.Condition.Accept(this);
        scope = scope.EnterScope(node);
        var body = node.Statements.Accept(this);
        scope.ExitScope();
        return condition && body;
    }

    public bool VisitFor(For node)
    {
        node.Scope = scope;
        scope = scope.EnterScope(node);
        var declaration = node.Declaration?.Accept(this);
        var assignment = node.Assignment?.Accept(this);
        var increment = node.Increment.Accept(this);
        var condition = node.Condition.Accept(this);
        var body = node.Statements?.Accept(this);
        scope = scope.ExitScope();
        return condition && (declaration != null || assignment != null) && increment;
    }

    public bool VisitSkip(Skip node)
    {
        throw new NotImplementedException();
    }

    public bool VisitUnaryOp(UnaryOp node)
    {
        throw new NotImplementedException();
    }

    public bool VisitString(String s)
    {
        return true;
    }

    public bool VisitType(Type type)
    {
        throw new NotImplementedException();
    }

    public bool VisitFunctionDeclaration(FunctionDeclaration functionDeclaration)
    {
        var identifier = functionDeclaration.Identifier.Name;
        scope = scope.EnterScope(functionDeclaration);
        var parameters = functionDeclaration.Declarations.Select(decl =>
        {
            if (!decl.Accept(this))
            {
                errors.Add("Line: " + decl.LineNumber + " Variable name: " + decl.Identifier.Name + " not found");
            }
            return new Variable(decl.Identifier.Name, decl.Expression);
        }).ToList();
        var statements = functionDeclaration.Statements;
        scope.ParentScope?.fTable.Bind(identifier, new Function(parameters, statements, scope));
        scope = scope.ExitScope();
        return true;
    }

    public bool VisitFunctionCall(FunctionCall functionCall)
    {
        functionCall.Scope = scope;
        var identifier = functionCall.Identifier.Name;
        var function = scope.fTable.LookUp(identifier);

        if (function == null)
        {
            errors.Add("Line: " + functionCall.LineNumber + " Function name: " + identifier + " not found");
        }

        scope = function?.Scope;
        var parameters = functionCall.Arguments.Select(expression => expression.Accept(this)).ToList();
        if (parameters.Count != function?.Parameters.Count)
        {
            errors.Add("Line: " + functionCall.LineNumber + " Function name: " + identifier + " has wrong number of arguments");
        }

        scope = functionCall.Scope;
        return parameters.All(p => p);
    }

    public bool VisitReturn(Return @return)
    {
        @return.Scope = scope;
        return @return.Expression.Accept(this);
    }

    public bool VisitNull(Null @null)
    {
        @null.Scope = scope;
        return true;
    }

    public bool VisitLine(Line line)
    {
        throw new NotImplementedException();
    }

    public bool VisitText(Text text)
    {
        throw new NotImplementedException();
    }

    public bool VisitCircle(Circle circle)
    {
        throw new NotImplementedException();
    }

    public bool VisitRectangle(Rectangle rectangle)
    {
        throw new NotImplementedException();
    }

    public bool VisitPoint(Point point)
    {
        throw new NotImplementedException();
    }

    public bool VisitColour(Colour colour)
    {
        throw new NotImplementedException();
    }

    public bool VisitSquare(Square square)
    {
        throw new NotImplementedException();
    }
}