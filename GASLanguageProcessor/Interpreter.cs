using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;
using Expression = GASLanguageProcessor.AST.Expressions.Expression;
using Number = GASLanguageProcessor.AST.Expressions.Terms.Number;

namespace GASLanguageProcessor;

public class Interpreter
{
    Dictionary<string, object> values = new();

    public object EvaluateStatement(Statement statement, Scope scope)
    {
        switch (statement)
        {
            case Compound compound:
                EvaluateStatement(compound.Statement1, compound.Scope ?? scope);
                EvaluateStatement(compound.Statement2, compound.Scope ?? scope);
                return null;
            case FunctionDeclaration functionDeclaration:
                return EvaluateStatement(functionDeclaration.Statements, functionDeclaration.Scope ?? scope);;
            case Declaration declaration:
                var val = EvaluateExpression(declaration.Expression, declaration.Scope ?? scope);
                var identifier = declaration.Identifier.Name;
                var variable = scope.vTable.LookUp(identifier);
                if (variable == null)
                {
                    Console.WriteLine($"Variable {identifier} not found");
                    return null;
                }
                values[identifier] = val;
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
                for (var i = 0; i < functionCall.Arguments.Count; i++)
                {
                    var val = EvaluateExpression(functionCall.Arguments[i], functionScope);
                    var identifier = function.Parameters[i].Identifier;
                    functionScope.vTable.Bind(identifier, new Variable(identifier, GasType.Number, val));
                }
                var functionCallRes = EvaluateStatement(function.Statements, functionScope);
                functionScope.vTable.Variables.Clear();
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
                var variable = scope.vTable.LookUp(identifier.Name);

                if (variable?.ActualValue != null)
                {
                    return variable.ActualValue;
                }
                return EvaluateExpression(scope.vTable.LookUp(identifier.Name)?.FormalValue, scope);

            case Number number:
                return float.Parse(number.Value);

            case Colour colour:
                var red = (float)EvaluateExpression(colour.Red, scope);
                var green = (float)EvaluateExpression(colour.Green, scope);
                var blue = (float)EvaluateExpression(colour.Blue, scope);
                var alpha = (float)EvaluateExpression(colour.Alpha, scope);
                return new {red, green, blue, alpha};

            case Point point:
                var x = (float)EvaluateExpression(point.X, scope);
                var y = (float)EvaluateExpression(point.Y, scope);
                return new {x, y};

            case Square square:
                var topLeft = EvaluateExpression(square.TopLeft, scope);
                var bottomRight = EvaluateExpression(square.BottomRight, scope);
                var stroke = (float) EvaluateExpression(square.Stroke, scope);
                var fillColour = EvaluateExpression(square.Colour, scope);
                var strokeColour = EvaluateExpression(square.StrokeColour, scope);
                return new { topLeft, bottomRight, stroke, fillColour, strokeColour };
            }

        return null;
    }
}
