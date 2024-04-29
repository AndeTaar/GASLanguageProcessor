﻿using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;
using Expression = GASLanguageProcessor.AST.Expressions.Expression;
using Number = GASLanguageProcessor.AST.Expressions.Terms.Number;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;

namespace GASLanguageProcessor;

public class Interpreter
{
    public object EvaluateStatement(Statement statement, Scope scope)
    {
        switch (statement)
        {
            case Canvas canvas:
                var width = (float) EvaluateExpression(canvas.Width, scope);
                var height = (float) EvaluateExpression(canvas.Height, scope);
                var backgroundColour = (FinalColour) EvaluateExpression(canvas.BackgroundColour, scope);
                return new FinalCanvas(width, height, backgroundColour);
            case Compound compound:
                EvaluateStatement(compound.Statement1, compound.Scope ?? scope);
                EvaluateStatement(compound.Statement2, compound.Scope ?? scope);
                return null;
            case FunctionDeclaration functionDeclaration:
                return null;
            case Declaration declaration:
                var val = EvaluateExpression(declaration.Expression, declaration.Scope ?? scope);
                var identifier = declaration.Identifier.Name;
                var variable = scope.vTable.LookUp(identifier);
                if (variable == null)
                {
                    Console.WriteLine($"Variable {identifier} not found");
                    return null;
                }
                variable.ActualValue = val;
                return val;
            case Return returnStatement:
                return EvaluateExpression(returnStatement.Expression, returnStatement.Scope ?? scope);
        }

        return null;
    }

    public object EvaluateExpression(Expression expression, Scope scope)
    {
        switch (expression)
        {
            case FunctionCall functionCall:
                Function function = scope.fTable.LookUp(functionCall.Identifier.Name);
                if (function == null)
                {
                    throw new Exception($"Function {functionCall.Identifier.Name} not found");
                }
                var functionScope = function.Scope;
                var functionCallRes = EvaluateStatement(function.Statements, functionScope);
                return functionCallRes;

            case BinaryOp binaryOp:
                var left = EvaluateExpression(binaryOp.Left, binaryOp.Scope ?? scope);
                var right = EvaluateExpression(binaryOp.Right, binaryOp.Scope ?? scope);

                return binaryOp.Op switch
                {
                    "+" => binaryOp.Type switch
                    {
                        GasType.Number => (float)left + (float)right,
                        GasType.String => (string)left + (string)right,
                        _ => throw new NotImplementedException()
                    },
                    "-" => (float)left - (float)right,
                    "*" => (float)left * (float)right,
                    "/" => (float)left / (float)right,
                    _ => throw new NotImplementedException()
                };

            case Identifier identifier:
                if (scope == null || scope.vTable == null || identifier == null || identifier.Name == null)
                {
                    throw new Exception("Scope, VariableTable, Identifier or Identifier Name is null");
                }

                var variable = scope.vTable.LookUp(identifier.Name);

                if (variable == null)
                {
                    throw new Exception($"Variable {identifier.Name} not found in the VariableTable");
                }

                if (variable.ActualValue != null)
                {
                    return variable.ActualValue;
                }
                return EvaluateExpression(variable.FormalValue, scope);

            case Number number:
                return float.Parse(number.Value);

            case String stringTerm:
                return stringTerm.Value;

            case Colour colour:
                var red = (float) EvaluateExpression(colour.Red, scope);
                var green = (float) EvaluateExpression(colour.Green, scope);
                var blue = (float) EvaluateExpression(colour.Blue, scope);
                var alpha = (float) EvaluateExpression(colour.Alpha, scope);
                return new FinalColour(red, green, blue, alpha);

            case Point point:
                var x = (float)EvaluateExpression(point.X, scope);
                var y = (float)EvaluateExpression(point.Y, scope);
                return new FinalPoint(x, y);

            case Square square:
                var topLeft = (FinalPoint) EvaluateExpression(square.TopLeft, scope);
                var length = (float) EvaluateExpression(square.Length, scope);
                var strokeSize = (float) EvaluateExpression(square.Stroke, scope);
                var squareFillColour = (FinalColour) EvaluateExpression(square.Colour, scope);
                var squareStrokeColour =(FinalColour) EvaluateExpression(square.StrokeColour, scope);
                return new FinalSquare(topLeft, length, strokeSize, squareFillColour, squareStrokeColour);

            case Text text:
                var value = (string) EvaluateExpression(text.Value, scope);
                var position = (FinalPoint) EvaluateExpression(text.Position, scope);
                var font = (string) EvaluateExpression(text.Font, scope);
                var fontSize = (float) EvaluateExpression(text.FontSize, scope);
                var textColour = (FinalColour) EvaluateExpression(text.Colour, scope);
                return new FinalText(value, position, font, fontSize, textColour);

            case Circle circle:
                var centre = (FinalPoint) EvaluateExpression(circle.Center, scope);
                var radius = (float) EvaluateExpression(circle.Radius, scope);
                var stroke = (float) EvaluateExpression(circle.Stroke, scope);
                var fillColour = (FinalColour) EvaluateExpression(circle.Colour, scope);
                var strokeColour = (FinalColour) EvaluateExpression(circle.StrokeColour, scope);
                return new FinalCircle(centre, radius, stroke, fillColour, strokeColour);

            case Rectangle rectangle:
                var rectTopLeft = (FinalPoint) EvaluateExpression(rectangle.TopLeft, scope);
                var rectBottomRight = (FinalPoint) EvaluateExpression(rectangle.BottomRight, scope);
                var rectStroke = (float) EvaluateExpression(rectangle.Stroke, scope);
                var rectFillColour = (FinalColour) EvaluateExpression(rectangle.Colour, scope);
                var rectStrokeColour = (FinalColour) EvaluateExpression(rectangle.StrokeColour, scope);
                return new FinalRectangle(rectTopLeft, rectBottomRight, rectStroke, rectFillColour, rectStrokeColour);

            case Line line:
                var lineStart = (FinalPoint) EvaluateExpression(line.Start, scope);
                var lineEnd = (FinalPoint) EvaluateExpression(line.End, scope);
                var lineStroke = (float) EvaluateExpression(line.Stroke, scope);
                var lineColour = (FinalColour) EvaluateExpression(line.Colour, scope);
                return new FinalLine(lineStart, lineEnd, lineStroke, lineColour);
        }

        return null;
    }
}
