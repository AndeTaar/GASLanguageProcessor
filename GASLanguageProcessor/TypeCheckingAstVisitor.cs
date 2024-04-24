using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;
using Boolean = GASLanguageProcessor.AST.Expressions.Boolean;
using String = GASLanguageProcessor.AST.Terms.String;
using Type = GASLanguageProcessor.AST.Terms.Type;

namespace GASLanguageProcessor;

public class TypeCheckingAstVisitor : IAstVisitor<GasType>
{
    //Everything is global scope for now
    private VariableTable vTable = new VariableTable();
    private FunctionTable fTable = new FunctionTable();

    public List<string> errors = new();

    public GasType VisitText(Text node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitBinaryOp(BinaryOp node)
    {
        var @operator = node.Op;
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

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

            case "&&" or "||" or "==" or "!=":
                if (left == GasType.Boolean && right == GasType.Boolean)
                    return GasType.Boolean;

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Boolean, got: " + left + " and " + right);
                return GasType.Error;

            default:
                errors.Add("Invalid operator: " + @operator);
                return GasType.Error;
        }
    }

    public GasType VisitCircle(Circle node)
    {
        return GasType.Circle;
    }

    public GasType VisitColour(Colour node)
    {
        return GasType.Colour;
    }

    public GasType VisitGroup(Group node)
    {
        node.Terms.ForEach(no => no.Accept(this));
        return GasType.Group;
    }

    public GasType VisitNumber(Number node)
    {
        return GasType.Number;
    }

    public GasType VisitPoint(Point node)
    {
        return GasType.Point;
    }

    public GasType VisitRectangle(Rectangle node)
    {
        return GasType.Rectangle;
    }

    public GasType VisitSquare(Square node)
    {
        return GasType.Square;
    }

    public GasType VisitLine(Line node)
    {
        return GasType.Line;
    }

    public GasType VisitIfStatement(If node)
    {
        var condition = node.Condition.Accept(this);
        node.Statements.Accept(this);
        node.Else?.Accept(this);

        if (condition != GasType.Boolean)
        {
            errors.Add("Invalid type for if condition: expected: Boolean, got: " + condition);
            return GasType.Error;
        }

        return GasType.Error;
    }

    public GasType VisitElseStatement(Else node)
    {
        node.Statements?.Accept(this);
        node.If?.Accept(this);

        return GasType.Error;
    }

    public GasType VisitBoolean(Boolean node)
    {
        return GasType.Boolean;
    }

    public GasType VisitIdentifier(Identifier node)
    {
        GasType type;
        try
        {
            type = vTable.Get(node.Name).Type;
        }
        catch (Exception e)
        {
            errors.Add("Variable name: " + node.Name + " not found");
            return GasType.Error;
        }
        return type;
    }

    public GasType VisitCompound(Compound node)
    {
        var left = node.Statement1?.Accept(this);
        var right = node.Statement2?.Accept(this);

        if (left != null)
        {
            return left ?? GasType.Error;
        }

        if (right != null)
        {
            return right ?? GasType.Error;
        }

        return GasType.Error;
    }

    public GasType VisitAssignment(Assignment node)
    {
        var left = node.Identifier.Name;
        var type = node.Value.Accept(this);

        var variable = vTable.Get(left);

        if (variable.Type != type)
        {
            throw new System.Exception("Invalid assignment");
        }

        return type;
    }

    public GasType VisitDeclaration(Declaration node)
    {
        var identifier = node.Identifier.Name;
        var type = node.Type.Accept(this);
        var value = node.Value;
        var typeOfValue = value?.Accept(this);
        if (type != typeOfValue && typeOfValue != null)
        {
            errors.Add("Invalid type for variable: " + identifier + " expected: " + type + " got: " + typeOfValue);
            return GasType.Error;
        }

        try
        {
            vTable.Add(identifier, new VariableType(type, value));
        }
        catch (Exception e)
        {
            errors.Add("Variable name: " + identifier + " is already declared elsewhere");
            return GasType.Error;
        }

        return type;
    }

    public GasType VisitCanvas(Canvas node)
    {
        var width = node.Width;
        var height = node.Height;
        var backgroundColourType = node.BackgroundColour.Accept(this);
        if (backgroundColourType != GasType.Colour)
        {
            errors.Add("Invalid background colour type");
            return GasType.Error;
        }

        return GasType.Canvas;
    }

    public GasType VisitWhile(While node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitSkip(Skip node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitUnaryOp(UnaryOp node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitString(String s)
    {
        return GasType.String;
    }

    public GasType VisitType(Type type)
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
        }
        errors.Add(type.Value + " Not implemented");
        return GasType.Error;
    }

    public GasType VisitFunctionDeclaration(FunctionDeclaration functionDeclaration)
    {
        var returnType = functionDeclaration.ReturnType.Accept(this);

        var identifier = functionDeclaration.Identifier;

        var parameterTypes = functionDeclaration.Declarations.Select(decl => decl.Accept(this)).ToList();

        fTable.Add(identifier.Name, new FunctionType(returnType, parameterTypes));

        return returnType;
    }

    public GasType VisitFunctionCall(FunctionCall functionCall)
    {
        var identifier = functionCall.Identifier;

        var parameterTypes = new List<GasType>();
        foreach (var parameter in functionCall.Parameters)
        {
            parameterTypes.Add(parameter.Accept(this));
        }

        FunctionType type;
        try
        {
            type = fTable.Get(identifier.Name);

        }
        catch (Exception e)
        {
            errors.Add("Function name: " + functionCall.Identifier.Name + " not found");
            return GasType.Error;
        }


        bool error = false;
        for (int i = 0; i < type.ParameterTypes.Count; i++)
        {
            if (type.ParameterTypes[i] != parameterTypes[i])
            {
                errors.Add("Invalid parameter " + i + " for function: " + identifier.Name + " expected: " + type.ParameterTypes[i] + " got: " + parameterTypes[i]);
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