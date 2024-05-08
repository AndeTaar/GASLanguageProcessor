﻿using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor;

public class TypeCheckingAstVisitor: IAstVisitor<GasType>
{
    public List<string> errors = new();

    public GasType VisitBinaryOp(BinaryOp node)
    {
        var @operator = node.Op;
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        switch (@operator)
        {
            case "+":
                if (left == GasType.String && right == GasType.String)
                {
                    node.Type = GasType.String;
                    return GasType.String;
                }

                if (left == GasType.Number && right == GasType.Number)
                {
                    node.Type = GasType.Number;
                    return GasType.Number;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: String or Number, got: " + left + " and " + right);
                return GasType.Error;

            case "-" or "*" or "/" or "%":
                if (left == GasType.Number && right == GasType.Number)
                {
                    node.Type = GasType.Number;
                    return GasType.Number;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Number, got: " + left + " and " + right);
                return GasType.Error;

            case "<" or ">" or "<=" or ">=":
                if (left == GasType.Number && right == GasType.Number)
                {
                    node.Type = GasType.Boolean;
                    return GasType.Boolean;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Number, got: " + left + " and " + right);
                return GasType.Error;

            case "&&" or "||":
                if (left == GasType.Boolean && right == GasType.Boolean)
                {
                    node.Type = GasType.Boolean;
                    return GasType.Boolean;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Boolean, got: " + left + " and " + right);
                return GasType.Error;

            case "==" or "!=":
                if ((left == GasType.Boolean && right == GasType.Boolean) ||
                    (left == GasType.Number && right == GasType.Number))
                {
                    node.Type = GasType.Boolean;
                    return GasType.Boolean;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Boolean, got: " + left + " and " + right);
                return GasType.Error;


            default:
                errors.Add("Invalid operator: " + @operator);
                return GasType.Error;
        }
    }

    public GasType VisitGroup(Group node)
    {
        node.Statements.Accept(this);
        var point = node.Point.Accept(this);

        if (point != GasType.Point)
        {
            errors.Add("Invalid type for point: expected: Point, got: " + point);
            return GasType.Error;
        }

        return GasType.Group;
    }

    public GasType VisitList(List node)
    {
        var typeOfElements = node.Expressions.Select(expr => expr.Accept(this)).ToList();

        var type = typeOfElements[0];
        foreach (var t in typeOfElements)
        {
            if (t != type)
            {
                errors.Add( "Line: " + node.LineNumber + " Not all elements in list are of same type");
            }
        }

        return type;
    }

    public GasType VisitAddToList(AddToList addToList)
    {
        throw new NotImplementedException();
    }

    public GasType VisitCollectionDeclaration(CollectionDeclaration collectionDeclaration)
    {
        var type = collectionDeclaration.Type.Accept(this);
        var identifier = collectionDeclaration.Identifier.Name;
        var expression = collectionDeclaration.Expression?.Accept(this);

        if (type != expression && expression != null)
        {
            errors.Add("Line: " + collectionDeclaration.LineNumber + " Invalid type for variable: " + identifier + " expected: " + type + " got: " + expression);
            return GasType.Error;
        }

        return type;
    }

    public GasType VisitAttributeAccess(Attribute attribute)
    {
        throw new NotImplementedException();
    }

    public GasType VisitNumber(Number node)
    {
        return GasType.Number;
    }

    public GasType VisitIfStatement(If node)
    {
        var condition = node.Condition.Accept(this);
        node.Statements?.Accept(this);
        node.Else?.Accept(this);

        if (condition != GasType.Boolean)
        {
            errors.Add("Line: " + node.LineNumber + " Invalid type for if condition: expected: Boolean, got: " + condition);
            return GasType.Error;
        }

        return GasType.Error;
    }

    public GasType VisitBoolean(Boolean node)
    {
        return GasType.Boolean;
    }

    public GasType VisitIdentifier(Identifier node)
    {
        var scope = node.Scope;
        GasType type;
        try
        {
            type = scope.vTable.LookUp(node.Name).Type;
        }
        catch (Exception e)
        {
            errors.Add("Line: " + node.LineNumber + " Variable name: " + node.Name + " not found");
            return GasType.Error;
        }
        return type;
    }

    public GasType VisitCompound(Compound node)
    {
        var left = node.Statement1?.Accept(this);
        var right = node.Statement2?.Accept(this);

        return GasType.Error;
    }

    public GasType VisitAssignment(Assignment node)
    {
        var scope = node.Scope;
        var identifier = node.Identifier.Name;
        var type = node.Expression.Accept(this);

        var variable = scope.vTable.LookUp(identifier);

        if (variable == null)
        {
            errors.Add("Variable name: " + identifier + " not found");
            return GasType.Error;
        }

        if (variable.Type != type)
        {
            errors.Add("Line: " + node.LineNumber + " Invalid type for variable: " + identifier + " expected: " + variable.Type + " got: " + type);
            return GasType.Error;
        }

        return type;
    }

    public GasType VisitDeclaration(Declaration node)
    {
        var scope = node.Scope;
        var identifier = node.Identifier.Name;
        var type = node.Type.Accept(this);
        var value = node.Expression;
        var typeOfValue = value?.Accept(this);
        if (type != typeOfValue && typeOfValue != null)
        {
            errors.Add("Line: " + node.LineNumber + " Invalid type for variable: " + identifier + " expected: " + type + " got: " + typeOfValue);
            return GasType.Error;
        }

        scope.vTable.SetType(identifier, type);
        return type;
    }

    public GasType VisitCanvas(Canvas node)
    {
        var width = node.Width.Accept(this);
        var height = node.Height.Accept(this);

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

        var backgroundColorType = node.BackgroundColor?.Accept(this);

        if (backgroundColorType != GasType.Color && backgroundColorType != null)
        {
            errors.Add("Invalid background color type");
            return GasType.Error;
        }

        return GasType.Canvas;
    }

    public GasType VisitWhile(While node)
    {
        var condition = node.Condition.Accept(this);
        node.Statements?.Accept(this);

        if (condition != GasType.Boolean)
        {
            errors.Add("Invalid type for while condition: expected: Boolean, got: " + condition);
            return GasType.Error;
        }

        return GasType.Error;
    }

    public GasType VisitFor(For node)
    {
        var assignment = node.Assignment?.Accept(this);
        var declaration = node.Declaration?.Accept(this);
        var condition = node.Condition.Accept(this);
        node.Statements?.Accept(this);

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

    public GasType VisitSkip(Skip node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitUnaryOp(UnaryOp node)
    {
        var expression = node.Expression?.Accept(this);
        var op = node.Op;

        switch (op)
        {
            case "!":
                if (expression == GasType.Boolean)
                {
                    return GasType.Boolean;
                }
                errors.Add("Invalid type for unary operation: " + op + " expected: Boolean, got: " + expression);
                return GasType.Error;
            case "-":
                if (expression == GasType.Number)
                {
                    return GasType.Number;
                }
                errors.Add("Invalid type for unary operation: " + op + " expected: Number, got: " + expression);
                return GasType.Error;
            default:
                errors.Add("Invalid operator: " + op);
                return GasType.Error;
        }
    }

    public GasType VisitString(String s)
    {
        return GasType.String;
    }

    public GasType VisitType(Type type)
    {
        type.Value = type.Value.Replace("list<", "").Replace(">", "");
        switch (type.Value)
        {
            case "number":
                return GasType.Number;
            case "string":
                return GasType.String;
            case "text":
                return GasType.Text;
            case "color":
                return GasType.Color;
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
            case "segLine":
                return GasType.SegLine;
            case "circle":
                return GasType.Circle;
            case "bool":
                return GasType.Boolean;
            case "group":
                return GasType.Group;
            case "ellipse":
                return GasType.Ellipse;
        }
        errors.Add(type.Value + " Not implemented");
        return GasType.Error;
    }

    public GasType VisitFunctionDeclaration(FunctionDeclaration functionDeclaration)
    {
        var scope = functionDeclaration.Scope;
        var returnType = functionDeclaration.ReturnType.Accept(this);
        var identifier = functionDeclaration.Identifier.Name;
        var function = scope.fTable.LookUp(identifier);

        if (function == null)
        {
            errors.Add(" Function name: " + identifier + " cannot be found");
            return GasType.Error;
        }

        var parameters = functionDeclaration.Declarations.Select(decl => decl.Accept(this)).ToList();

        scope.fTable.SetReturnType(identifier, returnType);
        scope.fTable.SetParameterTypes(identifier, parameters);
        return returnType;
    }

    public GasType VisitFunctionCallStatement(FunctionCallStatement functionCallStatement)
    {
        var scope = functionCallStatement.Scope;
        var functionAndIdentifier = scope?.LookupMethod(functionCallStatement.Identifier, scope, scope, errors);
        var identifier = functionAndIdentifier?.Item1;
        var function = functionAndIdentifier?.Item2;

        var parameterTypes = new List<GasType>();
        foreach (var parameter in functionCallStatement.Arguments)
        {
            parameterTypes.Add(parameter.Accept(this));
        }

        if (function.Parameters.Count != parameterTypes.Count)
        {
            errors.Add("Invalid number of parameters for function: " + identifier.Name + " expected: " + function.Parameters.Count + " got: " + parameterTypes.Count);
            return GasType.Error;
        }

        for (int i = 0; i < function.Parameters.Count; i++)
        {
            if (function.Parameters[i].Type != GasType.Any && function.Parameters[i].Type != parameterTypes[i])
            {
                errors.Add("Line: " + functionCallStatement.LineNumber + " Invalid parameter " + i + " for function: " + identifier.Name + " expected: " + function.Parameters[i].Type + " got: " + parameterTypes[i]);
            }
        }

        return function.ReturnType;
    }

    public GasType VisitFunctionCallTerm(FunctionCallTerm functionCallTerm)
    {
        var scope = functionCallTerm.Scope;
        var functionAndIdentifier = scope?.LookupMethod(functionCallTerm.Identifier, scope, scope, errors);
        var identifier = functionAndIdentifier?.Item1;
        var function = functionAndIdentifier?.Item2;

        var parameterTypes = new List<GasType>();
        foreach (var parameter in functionCallTerm.Arguments)
        {
            parameterTypes.Add(parameter.Accept(this));
        }


        if (function.Parameters.Count != parameterTypes.Count)
        {
            errors.Add("Invalid number of parameters for function: " + identifier + " expected: " + function.Parameters.Count + " got: " + parameterTypes.Count);
            return GasType.Error;
        }

        for (int i = 0; i < function.Parameters.Count; i++)
        {
            if (function.Parameters[i].Type != GasType.Any && function.Parameters[i].Type != parameterTypes[i])
            {
                errors.Add("Line: " + functionCallTerm.LineNumber + " Invalid parameter " + i + " for function: " + identifier.Name + " expected: " + function.Parameters[i].Type + " got: " + parameterTypes[i]);
            }
        }

        return function.ReturnType;
    }

    public GasType VisitReturn(Return @return)
    {
        return @return.Expression.Accept(this);
    }

    public GasType VisitNull(Null @null)
    {
        return GasType.Null;
    }

    public GasType VisitLine(SegLine segLine)
    {
        throw new NotImplementedException();
    }

    public GasType VisitText(Text text)
    {
        throw new NotImplementedException();
    }

    public GasType VisitCircle(Circle circle)
    {
        throw new NotImplementedException();
    }

    public GasType VisitRectangle(Rectangle rectangle)
    {
        throw new NotImplementedException();
    }

    public GasType VisitPoint(Point point)
    {
        throw new NotImplementedException();
    }

    public GasType VisitColor(Color color)
    {
        throw new NotImplementedException();
    }

    public GasType VisitSquare(Square square)
    {
        throw new NotImplementedException();
    }

    public GasType VisitEllipse(Ellipse ellipse)
    {
        throw new NotImplementedException();
    }

    public GasType VisitSegLine(SegLine segLine)
    {
        throw new NotImplementedException();
    }

    public GasType VisitLine(Line line)
    {
        throw new NotImplementedException();
    }
}
