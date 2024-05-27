using System.Globalization;
using Antlr4.Runtime;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using Expression = GASLanguageProcessor.AST.Expressions.Expression;
using Num = GASLanguageProcessor.AST.Expressions.Terms.Num;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;

namespace GASLanguageProcessor;

public class Interpreter
{
    public float canvasWidth;
    public float canvasHeight;
    public List<string> errors = new();

    public object? EvaluateStatement(Statement statement, Scope scope)
    {
        switch (statement)
        {
            case Canvas canvas:
                var width = (float)EvaluateExpression(canvas.Width, scope);
                var height = (float)EvaluateExpression(canvas.Height, scope);
                canvasHeight = height;
                canvasHeight = height;
                canvasWidth = width;
                var backgroundColor = canvas.BackgroundColor == null
                    ? new FinalColor(255, 255, 255, 1)
                    : (FinalColor)EvaluateExpression(canvas.BackgroundColor, scope);
                var finalCanvas = new FinalCanvas(width, height, backgroundColor);
                var canvasIndex = scope.vTable.LookUp("canvas");
                if(canvasIndex == null)
                {
                    errors.Add("Canvas value not found.");
                    return null;
                }
                var value = scope.stoTable.LookUp(canvasIndex.Value);
                value.ActualValue = finalCanvas;
                return null;

            case Compound compound:
                var eval1 = EvaluateStatement(compound.Statement1, compound.Scope ?? scope);
                if(eval1 != null) return eval1;
                var eval2 = EvaluateStatement(compound.Statement2, compound.Scope ?? scope);
                return eval1 ?? eval2;

            // Currently allows infinite loops.
            case For @for:
                EvaluateStatement(@for.Initializer, @for.Scope ?? scope);
                var condition = EvaluateExpression(@for.Condition, @for.Scope ?? scope);
                while ((bool)condition)
                {
                    var eval = EvaluateStatement(@for.Statements, @for.Scope ?? scope);
                    if (eval != null) return eval;
                    EvaluateStatement(@for.Incrementer, @for.Scope ?? scope);
                    condition = EvaluateExpression(@for.Condition, @for.Scope ?? scope);
                }

                return null;

            // Currently allows infinite loops.
            case While @while:
                var whileCondition = EvaluateExpression(@while.Condition, @while.Scope ?? scope);
                while ((bool)whileCondition)
                {
                    var eval = EvaluateStatement(@while.Statements, @while.Scope ?? scope);
                    if (eval != null) return eval;
                    whileCondition = EvaluateExpression(@while.Condition, @while.Scope ?? scope);
                }
                return null;
            case If @if:
                var ifCondition = EvaluateExpression(@if.Condition, @if.Scope ?? scope);

                if ((bool)ifCondition)
                {
                    return EvaluateStatement(@if.Statements, @if.Scope ?? scope);
                }
                if (@if.Else != null)
                {
                    return EvaluateStatement(@if.Else, @if.Scope ?? scope);
                }

                return null;
            case FunctionDeclaration functionDeclaration:
                return null;
            case Declaration declaration:
                var val = EvaluateExpression(declaration.Expression, declaration.Scope ?? scope);
                var declIdentifier = declaration.Identifier.Name;
                var variableIndex = scope.vTable.LookUp(declIdentifier);
                if (variableIndex == null)
                {
                    errors.Add($"Variable {declIdentifier} not found");
                    return null;
                }
                var variable = scope.stoTable.LookUp(variableIndex.Value);
                variable.ActualValue = val;
                return null;

            case FunctionCallStatement functionCallStatement:
                var fcIdentifier = functionCallStatement.Identifier;
                var function = scope.fTable.LookUp(fcIdentifier.Name);
                if (function == null)
                {
                    errors.Add($"Function {fcIdentifier} not found");
                    return null;
                }

                var functionCallScope = functionCallStatement.Scope ?? scope;
                var functionScope = function.Scope;
                for (int i = 0; i < function.Parameters.Count; i++)
                {
                    var parameter = function.Parameters[i];
                    var functionParameterVal = EvaluateExpression(functionCallStatement.Arguments[i], functionCallScope);
                    var varIndex = functionScope.vTable.LocalLookUp(parameter.Identifier);
                    var var = functionScope.stoTable.LookUp(varIndex.Value);

                    if (var != null) // IDE Says this is always true, but i dont see how that is possible.
                    {
                        var.ActualValue = functionParameterVal;
                    }
                    else
                    {
                        functionScope.vTable.Bind(parameter.Identifier,
                            varIndex.Value);
                    }
                }

                EvaluateStatement(function.Statements, functionScope);
                return null;

            case Assignment assignment:
                var assignExpression = EvaluateExpression(assignment.Expression, assignment.Scope ?? scope);
                var assignIdentifier = assignment.Identifier.Name;
                var assignIndex = scope.vTable.LookUp(assignIdentifier);

                if (assignIndex == null)
                {
                    errors.Add($"Variable {assignIdentifier} not found");
                    return null;
                }
                var assignVariable = scope.stoTable.LookUp(assignIndex.Value);


                switch (assignment.Operator)
                {
                    case "+=":
                        assignVariable.ActualValue = (float)assignVariable.ActualValue! + (float)assignExpression;
                        break;
                    case "-=":
                        assignVariable.ActualValue = (float)assignVariable.ActualValue! - (float)assignExpression;
                        break;
                    case "*=":
                        assignVariable.ActualValue = (float)assignVariable.ActualValue! * (float)assignExpression;
                        break;
                    case "/=":
                        assignVariable.ActualValue = (float)assignVariable.ActualValue! / (float)assignExpression;
                        break;
                    case "=":
                        assignVariable.ActualValue = assignExpression;
                        break;
                }

                return null;

            case Increment increment:
                var incrementIdentifier = increment.Identifier.Name;
                var incrementVariableIndex = scope.vTable.LookUp(incrementIdentifier);
                if (incrementVariableIndex == null)
                {
                    errors.Add($"Variable {incrementIdentifier} not found");
                    return null;
                }
                var incrementVariable = scope.stoTable.LookUp(incrementVariableIndex.Value);
                var op = increment.Operator;

                switch (op)
                {
                    case "++":
                        incrementVariable.ActualValue = (float)incrementVariable.ActualValue! + 1;
                        break;
                    case "--":
                        incrementVariable.ActualValue = (float)incrementVariable.ActualValue! - 1;
                        break;
                }

                return null;

            case Return returnStatement:
                return EvaluateExpression(returnStatement.Expression, returnStatement.Scope ?? scope);
        }

        return null;
    }

    public object EvaluateExpression(Expression expression, Scope scope)
    {
        switch (expression)
        {
            case FunctionCallTerm functionCall:
                Function function = scope.fTable.LookUp(functionCall.Identifier.Name);
                if (function == null)
                {
                    errors.Add($"Function {functionCall.Identifier.Name} not found");
                    return null;
                }

                var functionCallScope = functionCall.Scope ?? scope;
                var functionScope = function.Scope;

                if (function.Parameters.Count != functionCall.Arguments.Count)
                {
                    errors.Add($"Function {functionCall.Identifier.Name} has {function.Parameters.Count} parameters, but {functionCall.Arguments.Count} arguments were provided");
                    return null;
                }

                for (int i = 0; i < function.Parameters.Count; i++)
                {
                    var parameter = function.Parameters[i];
                    var functionCallVal = EvaluateExpression(functionCall.Arguments[i], functionCallScope);
                    var varIndex = functionScope.vTable.LocalLookUp(parameter.Identifier);
                    if(varIndex == null)
                    {
                        errors.Add($"Variable {parameter.Identifier} not found");
                        return null;
                    }
                    var var = functionScope.stoTable.LookUp(varIndex.Value);
                    if (var != null)
                    {
                        var.ActualValue = functionCallVal;
                    }
                    else
                    {
                        functionScope.vTable.Bind(parameter.Identifier, varIndex.Value);
                    }
                }

                var functionCallRes = EvaluateStatement(function.Statements, functionScope);
                return functionCallRes;

            case UnaryOp unaryOp:
                var unaryOpExpression = EvaluateExpression(unaryOp.Expression, scope);
                return unaryOp.Op switch
                {
                    "-" => -(float)unaryOpExpression,
                    "!" => !(bool)unaryOpExpression,
                    _ => throw new NotImplementedException()
                };

            case BinaryOp binaryOp:
                var left = EvaluateExpression(binaryOp.Left, scope);
                var right = EvaluateExpression(binaryOp.Right, scope);

                if((binaryOp.Op == "/" || binaryOp.Op == "%") && (float)right == 0)
                {
                    throw new Exception("Division by zero is not allowed.");
                }

                return binaryOp.Op switch
                {
                    "+" => binaryOp.Type switch
                    {
                        GasType.Num => (float)left + (float)right,
                        GasType.String => (string)left + (string)right,
                        _ => (float)left + (float)right
                    },
                    "-" => (float)left - (float)right,
                    "*" => (float)left * (float)right,
                    "/" => (float)left / (float)right,
                    "%" => (float)left % (float)right,
                    "<" => (float)left < (float)right,
                    ">" => (float)left > (float)right,
                    "<=" => (float)left <= (float)right,
                    ">=" => (float)left >= (float)right,
                    "!=" => !left.Equals(right),
                    "==" => left.Equals(right),
                    "&&" => (bool)left && (bool)right,
                    "||" => (bool)left || (bool)right,
                    _ => throw new NotImplementedException()
                };

            case Identifier identifier:
                if (scope == null || scope.vTable == null || identifier == null || identifier.Name == null)
                {
                    errors.Add("Scope, VariableTable, Identifier or Identifier Name is null");
                }

                var variableIndex = scope.vTable.LookUp(identifier.Name);
                var variable = scope.stoTable.LookUp(variableIndex.Value);

                if (variable == null)
                {
                    errors.Add($"Variable {identifier.Name} not found in the VariableTable");
                    return null;
                }

                if (variable.ActualValue != null)
                {
                    return variable.ActualValue;
                }

                return EvaluateExpression(variable.FormalValue, scope);

            case Num num: // Num is a float; CultureInfo is used to ensure that the decimal separator is a dot
                return float.Parse(num.Value, CultureInfo.InvariantCulture);

            case Boolean boolean:
                return bool.Parse(boolean.Value);

            case String stringTerm:
                return stringTerm.Value.TrimStart('"').TrimEnd('"').Replace('\\', ' ');

            case Color color:
                var red = (float)EvaluateExpression(color.Red, scope);
                var green = (float)EvaluateExpression(color.Green, scope);
                var blue = (float)EvaluateExpression(color.Blue, scope);
                var alpha = (float)EvaluateExpression(color.Alpha, scope);
                return new FinalColor(red, green, blue, alpha);

            case Point point:
                var x = (float)EvaluateExpression(point.X, scope);
                var y = (float)EvaluateExpression(point.Y, scope);
                return new FinalPoint(x, y);

            case Square square:
                var topLeft = (FinalPoint)EvaluateExpression(square.TopLeft, scope);
                var length = (float)EvaluateExpression(square.Length, scope);
                var strokeSize = (float)EvaluateExpression(square.Stroke, scope);
                var squareFillColor = (FinalColor)EvaluateExpression(square.Color, scope);
                var squareStrokeColor = (FinalColor)EvaluateExpression(square.StrokeColor, scope);
                var cornerRounding = (float)EvaluateExpression(square.CornerRounding, scope);
                return new FinalSquare(topLeft, length, strokeSize, squareFillColor, squareStrokeColor, cornerRounding);

            case Ellipse ellipse:
                var ellipseCentre = (FinalPoint)EvaluateExpression(ellipse.Center, scope);
                var ellipseRadiusX = (float)EvaluateExpression(ellipse.RadiusX, scope);
                var ellipseRadiusY = (float)EvaluateExpression(ellipse.RadiusY, scope);
                var ellipseStroke = (float)EvaluateExpression(ellipse.Stroke, scope);
                var ellipseFillColor = (FinalColor)EvaluateExpression(ellipse.Color, scope);
                var ellipseStrokeColor = (FinalColor)EvaluateExpression(ellipse.StrokeColor, scope);
                return new FinalEllipse(ellipseCentre, ellipseRadiusX, ellipseRadiusY, ellipseStroke, ellipseFillColor,
                    ellipseStrokeColor);

            case Text text:
                var value = (string)EvaluateExpression(text.Value, scope);
                var position = (FinalPoint)EvaluateExpression(text.Position, scope);
                var font = (string)EvaluateExpression(text.Font, scope);
                var fontSize = (float)EvaluateExpression(text.FontSize, scope);
                var fontWeight = (float)EvaluateExpression(text.FontWeight, scope);
                var textColor = (FinalColor)EvaluateExpression(text.Color, scope);
                return new FinalText(value, position, font, fontSize, fontWeight, textColor);

            case Circle circle:
                var centre = (FinalPoint)EvaluateExpression(circle.Center, scope);
                var radius = (float)EvaluateExpression(circle.Radius, scope);
                var stroke = (float)EvaluateExpression(circle.Stroke, scope);
                var fillColor = (FinalColor)EvaluateExpression(circle.Color, scope);
                var strokeColor = (FinalColor)EvaluateExpression(circle.StrokeColor, scope);
                return new FinalCircle(centre, radius, stroke, fillColor, strokeColor);

            case Rectangle rectangle:
                var rectTopLeft = (FinalPoint)EvaluateExpression(rectangle.TopLeft, scope);
                var rectBottomRight = (FinalPoint)EvaluateExpression(rectangle.BottomRight, scope);
                var rectStroke = (float)EvaluateExpression(rectangle.Stroke, scope);
                var rectFillColor = (FinalColor)EvaluateExpression(rectangle.Color, scope);
                var rectStrokeColor = (FinalColor)EvaluateExpression(rectangle.StrokeColor, scope);
                var rectCornerRounding = (float)EvaluateExpression(rectangle.CornerRounding, scope);
                return new FinalRectangle(rectTopLeft, rectBottomRight, rectStroke, rectFillColor, rectStrokeColor, rectCornerRounding);

            case Line line:
                var lineIntercept = (float)EvaluateExpression(line.Intercept, scope);
                var lineGradient = (float)EvaluateExpression(line.Gradient, scope);
                var lineStart = new FinalPoint(-1, lineIntercept-lineGradient);

                float lineEndX = lineGradient < 0
                    ? canvasWidth - Math.Abs((canvasHeight - lineIntercept) / lineGradient) + 1
                    : Math.Abs((canvasHeight - lineIntercept) / lineGradient) + 1;
                float lineEndY = lineGradient * lineEndX + lineIntercept;
                var lineEnd = new FinalPoint(lineEndX, lineEndY);

                var lineStroke = (float)EvaluateExpression(line.Stroke, scope);
                var lineColor = (FinalColor)EvaluateExpression(line.Color, scope);
                return new FinalLine(lineStart, lineEnd, lineStroke, lineColor);

            case SegLine segLine:
                var segLineStart = (FinalPoint)EvaluateExpression(segLine.Start, scope);
                var segLineEnd = (FinalPoint)EvaluateExpression(segLine.End, scope);
                var segLineStroke = (float)EvaluateExpression(segLine.Stroke, scope);
                var segLineColor = (FinalColor)EvaluateExpression(segLine.Color, scope);
                return new FinalSegLine(segLineStart, segLineEnd, segLineStroke, segLineColor);

            case Arrow arrow:
                var arrowStart = (FinalPoint)EvaluateExpression(arrow.Start, scope);
                var arrowEnd = (FinalPoint)EvaluateExpression(arrow.End, scope);
                var arrowStroke = (float)EvaluateExpression(arrow.Stroke, scope);
                var arrowColor = (FinalColor)EvaluateExpression(arrow.Color, scope);
                return new FinalArrow(arrowStart, arrowEnd, arrowStroke, arrowColor);

            case Polygon polygon:
                var polygonPoints = (FinalList)EvaluateExpression(polygon.Points, scope);
                var polygonColor = (FinalColor)EvaluateExpression(polygon.Color, scope);
                var polygonStroke = (float)EvaluateExpression(polygon.Stroke, scope);
                var polygonStrokeColor = (FinalColor)EvaluateExpression(polygon.StrokeColor, scope);
                return new FinalPolygon(polygonPoints, polygonStroke, polygonColor, polygonStrokeColor);

            case Group group:
                var finalPoint = (FinalPoint)EvaluateExpression(group.Point, scope);
                EvaluateStatement(group.Statements, group.Scope ?? scope);
                return new FinalGroup(finalPoint, group.Scope ?? scope);

            case AddToList addToList:
                var listVariableIndex = scope.vTable.LookUp(addToList.ListIdentifier.Name);
                var listVariable = scope.stoTable.LookUp(listVariableIndex.Value);

                if (listVariable == null)
                {
                    errors.Add($"Variable {addToList.ListIdentifier.Name} not found");
                    return null;
                }

                if (listVariable.ActualValue == null)
                {
                    errors.Add($"Variable {addToList.ListIdentifier.Name} is not initialized");
                    return null;
                }

                if (listVariable.ActualValue is not FinalList destList)
                {
                    errors.Add($"Variable {addToList.ListIdentifier.Name} is not a list");
                    return null;
                }

                var valueToAdd = EvaluateExpression(addToList.Value, addToList.Scope ?? scope);
                destList.Values.Add(valueToAdd);
                return null;

            case RemoveFromList removeFromList:
                var listToRemoveFromIndex = scope.vTable.LookUp(removeFromList.ListIdentifier.Name);
                var listToRemoveFrom = scope.stoTable.LookUp(listToRemoveFromIndex.Value);
                var indexToRemove =
                    Convert.ToInt32(EvaluateExpression(removeFromList.Index, removeFromList.Scope ?? scope));

                if (listToRemoveFrom == null)
                {
                    errors.Add($"Variable {removeFromList.ListIdentifier.Name} not found");
                    return null;
                }

                if (listToRemoveFrom.ActualValue is not FinalList destinedList)
                {
                    errors.Add($"Variable {removeFromList.ListIdentifier.Name} is not a list");
                    return null;
                }

                if (indexToRemove < 0 || indexToRemove >= destinedList.Values.Count)
                {
                    errors.Add($"Index {indexToRemove} out of range for list {removeFromList.ListIdentifier.Name}");
                    return null;
                }

                destinedList.Values.RemoveAt(indexToRemove);
                return null;


            case GetFromList getFromList:
                var listToGetFromIndex = scope.vTable.LookUp(getFromList.ListIdentifier.Name);
                var listToGetFrom = scope.stoTable.LookUp(listToGetFromIndex.Value);
                var indexOfValue = Convert.ToInt32(EvaluateExpression(getFromList.Index, getFromList.Scope ?? scope));

                if (listToGetFrom == null)
                {
                    errors.Add($"Variable {getFromList.ListIdentifier.Name} not found");
                    return null;
                }

                if (listToGetFrom.ActualValue is not FinalList sourceList)
                {
                    errors.Add($"Variable {getFromList.ListIdentifier.Name} is not a list");
                    return null;
                }

                if (indexOfValue < 0 || indexOfValue >= sourceList.Values.Count)
                {
                    errors.Add($"Index {indexOfValue} out of range for list {getFromList.ListIdentifier.Name}");
                    return null;
                }

                var valueToGet = sourceList.Values[indexOfValue];

                return valueToGet;

            case LengthOfList lengthOfList:
                var listToCheckIndex = scope.vTable.LookUp(lengthOfList.ListIdentifier.Name);
                var listToCheck = scope.stoTable.LookUp(listToCheckIndex.Value);

                if (listToCheck == null)
                {
                    errors.Add($"Variable {lengthOfList.ListIdentifier.Name} not found");
                    return null;
                }

                if (listToCheck.ActualValue is not FinalList listToCheckLength)
                {
                    errors.Add($"Variable {lengthOfList.ListIdentifier.Name} is not a list");
                    return null;
                }

                return (float)listToCheckLength.Values.Count;

            case List list:
                var values = new List<object>();
                foreach (var expr in list.Expressions)
                {
                    values.Add(EvaluateExpression(expr, list.Scope ?? scope));
                }

                return new FinalList(values, list.Scope ?? scope);
        }

        return null;
    }
}