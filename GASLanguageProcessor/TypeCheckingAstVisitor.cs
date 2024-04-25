﻿using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;
using Boolean = GASLanguageProcessor.AST.Expressions.Boolean;
using String = GASLanguageProcessor.AST.Terms.String;
using Type = GASLanguageProcessor.AST.Terms.Type;

namespace GASLanguageProcessor;

public class TypeCheckingAstVisitor : IAstVisitor<GasType>
{
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

    public GasType VisitAssignment(Assignment node, Scope scope)
    {
        var left = node.Identifier.Name;
        var type = node.Value.Accept(this);

        var variable = scope.vTable.LookUp(left);

        if (variable.Type != type)
        {
            throw new System.Exception("Invalid assignment");
        }

        return type;
    }

    public GasType VisitDeclaration(Declaration node, VariableTable vTable, FunctionTable fTable)
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
            vTable.Bind(identifier, new VariableType(type, value));
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

    public GasType VisitWhile(While node, Scope scope)
    {
        var condition = node.Condition.Accept(this, scope);
        var whileScope = scope.EnterScope();
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
        var scopeFor = scope.EnterScope();
        var initializer = node.Initializer.Accept(this, scopeFor);
        var condition = node.Condition.Accept(this, scopeFor);
        var increment = node.Increment.Accept(this, scopeFor);
        node.Body.Accept(this, scopeFor);

        if(initializer != GasType.Number)
        {
            errors.Add("Invalid type for for initializer: expected: Number, got: " + initializer);
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

        var funcDeclScope = scope.EnterScope();

        var parameterTypes = functionDeclaration.Declarations.Select(decl => decl.Accept(this, funcDeclScope)).ToList();

        var returnStatement = functionDeclaration.ReturnStatement?.Accept(this, funcDeclScope);

        if (returnType != returnStatement && returnType != GasType.Null && returnStatement != GasType.Null && returnType != GasType.Void)
        {
            errors.Add("Invalid return type for function: " + identifier.Name + " expected: " + returnType + " got: " + returnStatement);
            return GasType.Error;
        }

        scope.fTable.Bind(identifier.Name, new FunctionType(returnType, parameterTypes));

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

    public GasType VisitFunctionCall(FunctionCall functionCall, Scope scope)
    {
        var identifier = functionCall.Identifier;

        var parameterTypes = new List<GasType>();
        foreach (var parameter in functionCall.Parameters)
        {
            parameterTypes.Add(parameter.Accept(this, scope));
        }

        var type = scope.GlobalLookupFunction(identifier.Name);

        if(type == null){
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