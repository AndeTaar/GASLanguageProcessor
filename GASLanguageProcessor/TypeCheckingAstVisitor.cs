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

public class TypeCheckingAstVisitor : IAstVisitor<GasType>
{
    public List<string> errors = new();

    public GasType VisitBinaryOp(BinaryOp node, Scope scope)
    {
        var @operator = node.Op;
        var left = node.Left.Accept(this, scope);
        var right = node.Right.Accept(this, scope);

        switch (@operator)
        {
            case "+":
                if (left == GasType.String && right == GasType.String)
                    return GasType.String;

                if (left == GasType.Number && right == GasType.Number)
                    return GasType.Number;

                errors.Add("Invalid types for binary operation: " + @operator + " expected: String or Number, got: " + left + " and " + right);
                return GasType.Error;

            case "-" or "*" or "/":
                if (left == GasType.Number && right == GasType.Number)
                    return GasType.Number;

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Number, got: " + left + " and " + right);
                return GasType.Error;

            case "<" or ">" or "<=" or ">=":
                if (left == GasType.Number && right == GasType.Number)
                    return GasType.Boolean;

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Number, got: " + left + " and " + right);
                return GasType.Error;

            case "&&" or "||":
                if (left == GasType.Boolean && right == GasType.Boolean)
                    return GasType.Boolean;

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Boolean, got: " + left + " and " + right);
                return GasType.Error;

            case "==" or "!=":
                if ((left == GasType.Boolean && right == GasType.Boolean) ||
                    (left == GasType.Number && right == GasType.Number))
                    return GasType.Boolean;

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Boolean, got: " + left + " and " + right);
                return GasType.Error;


            default:
                errors.Add("Invalid operator: " + @operator);
                return GasType.Error;
        }
    }

    public GasType VisitGroup(Group node, Scope scope)
    {
        node.Terms.ForEach(no => no.Accept(this, scope));
        return GasType.Group;
    }

    public GasType VisitNumber(Number node, Scope scope)
    {
        return GasType.Number;
    }

    public GasType VisitIfStatement(If node, Scope scope)
    {
        var condition = node.Condition.Accept(this, scope);
        var ifScope = scope.EnterScope(node);
        node.Statements?.Accept(this, ifScope);
        node.Else?.Accept(this, scope);

        if (condition != GasType.Boolean)
        {
            errors.Add("Invalid type for if condition: expected: Boolean, got: " + condition);
            return GasType.Error;
        }

        return GasType.Error;
    }

    public GasType VisitBoolean(Boolean node, Scope scope)
    {
        return GasType.Boolean;
    }

    public GasType VisitIdentifier(Identifier node, Scope scope)
    {
        GasType type;
        try
        {
            type = scope.vTable.LookUp(node.Name).Type;
        }
        catch (Exception e)
        {
            errors.Add("Variable name: " + node.Name + " not found");
            return GasType.Error;
        }
        return type;
    }

    public GasType VisitCompound(Compound node, Scope scope)
    {
        var left = node.Statement1?.Accept(this, scope);
        var right = node.Statement2?.Accept(this, scope);

        return GasType.Error;
    }

    public GasType VisitAssignment(Assignment node, Scope scope)
    {
        var left = node.Identifier.Name;
        var type = node.Value.Accept(this, scope);

        var variable = scope.vTable.LookUp(left);

        if (variable == null)
        {
            errors.Add("Variable name: " + left + " not found");
            return GasType.Error;
        }

        if (variable.Type != type)
        {
            errors.Add("Invalid type for variable: " + left + " expected: " + variable.Type + " got: " + type);
            return GasType.Error;
        }

        return type;
    }

    public GasType VisitDeclaration(Declaration node, Scope scope)
    {
        var identifier = node.Identifier.Name;
        var type = node.Type.Accept(this, scope);
        var value = node.Expression;
        var typeOfValue = value?.Accept(this, scope);
        if (type != typeOfValue && typeOfValue != null)
        {
            errors.Add("Invalid type for variable: " + identifier + " expected: " + type + " got: " + typeOfValue);
            return GasType.Error;
        }

        if (scope.vTable.LookUp(identifier) != null)
        {
            errors.Add("Variable name: " + identifier + " is already declared elsewhere");
            return GasType.Error;
        }

        scope.vTable.Bind(identifier, new Variable(identifier, type, value));

        return type;
    }

    public GasType VisitCanvas(Canvas node, Scope scope)
    {
        var width = node.Width.Accept(this, scope);
        var height = node.Height.Accept(this, scope);
        
        if (width != GasType.Number)
        {
            errors.Add("Invalid width type");
            return GasType.Error;
        }
        
        if (height != GasType.Number)
        {
            errors.Add("Invalid height type");
            return GasType.Error;
        }
        
        var backgroundColourType = node.BackgroundColour.Accept(this, scope);
        if (backgroundColourType != GasType.Colour)
        {
            errors.Add("Invalid background colour type");
            return GasType.Error;
        }
        
        return GasType.Canvas;
    }

    public GasType VisitWhile(While node, Scope scope)
    {
        var condition = node.Condition.Accept(this, scope);
        var whileScope = scope.EnterScope(node);
        node.Statements.Accept(this, whileScope);

        if (condition != GasType.Boolean)
        {
            errors.Add("Invalid type for while condition: expected: Boolean, got: " + condition);
            return GasType.Error;
        }

        return GasType.Error;
    }

    public GasType VisitFor(For node, Scope scope)
    {
        var scopeFor = scope.EnterScope(node);
        var assignment = node.Assignment?.Accept(this, scopeFor);
        var declaration = node.Declaration?.Accept(this, scopeFor);
        var condition = node.Condition.Accept(this, scopeFor);
        node.Body.Accept(this, scopeFor);

        if(assignment != GasType.Number && declaration != GasType.Number)
        {
            errors.Add("Invalid type for for initializer: expected: Number, got: " + (declaration == null ? declaration : assignment));
            return GasType.Error;
        }

        if (condition != GasType.Boolean)
        {
            errors.Add("Invalid type for for condition: expected: Boolean, got: " + condition);
            return GasType.Error;
        }

        return GasType.Error;
    }

    public GasType VisitSkip(Skip node, Scope scope)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitUnaryOp(UnaryOp node, Scope scope)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitString(String s, Scope scope)
    {
        return GasType.String;
    }

    public GasType VisitType(Type type, Scope scope)
    {
        switch (type.Value)
        {
            case "number":
                return GasType.Number;
            case "text":
                return GasType.Text;
            case "colour":
                return GasType.Colour;
            case "boolean":
                return GasType.Boolean;
            case "square":
                return GasType.Square;
            case "rectangle":
                return GasType.Rectangle;
            case "point":
                return GasType.Point;
            case "line":
                return GasType.Line;
            case "circle":
                return GasType.Circle;
            case "bool":
                return GasType.Boolean;
        }
        errors.Add(type.Value + " Not implemented");
        return GasType.Error;
    }

    public GasType VisitFunctionDeclaration(FunctionDeclaration functionDeclaration, Scope scope)
    {
        var returnType = functionDeclaration.ReturnType.Accept(this, scope);

        var identifier = functionDeclaration.Identifier;

        var funcDeclScope = scope.EnterScope(functionDeclaration);

        var parameters = functionDeclaration.Declarations.Select(decl => new Variable(decl.Identifier.Name,
            decl.Accept(this, funcDeclScope), null)).ToList();

        var statements = functionDeclaration.Statements;
        scope.fTable.Bind(identifier.Name, new Function(returnType, parameters, statements, funcDeclScope));

        return returnType;
    }

    public GasType VisitReturn(Return @return, Scope scope)
    {
        return @return.Expression.Accept(this, scope);
    }

    public GasType VisitNull(Null @null, Scope scope)
    {
        return GasType.Null;
    }

    public GasType VisitLine(Line line, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitText(Text text, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitCircle(Circle circle, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitRectangle(Rectangle rectangle, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitPoint(Point point, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitColour(Colour colour, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitSquare(Square square, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitFunctionCall(FunctionCall functionCall, Scope scope)
    {
        var identifier = functionCall.Identifier;

        var parameterTypes = new List<GasType>();
        foreach (var parameter in functionCall.Arguments)
        {
            parameterTypes.Add(parameter.Accept(this, scope));
        }

        var type = scope.fTable.LookUp(identifier.Name);

        if(type == null){
            errors.Add("Function name: " + functionCall.Identifier.Name + " not found");
            return GasType.Error;
        }

        if (type.Parameters.Count != parameterTypes.Count)
        {
            errors.Add("Invalid number of parameters for function: " + identifier.Name + " expected: " + type.Parameters.Count + " got: " + parameterTypes.Count);
            return GasType.Error;
        }

        bool error = false;
        for (int i = 0; i < type.Parameters.Count; i++)
        {
            if (type.Parameters[i].Type != parameterTypes[i])
            {
                errors.Add("Invalid parameter " + i + " for function: " + identifier.Name + " expected: " + type.Parameters[i].Type + " got: " + parameterTypes[i]);
                error = true;
            }
        }

        if (error)
        {
            return GasType.Error;
        }

        return type.ReturnType;
    }
}
