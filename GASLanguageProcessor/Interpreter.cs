using System.Globalization;
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
                var canvasVariable = scope.vTable.LookUp("canvas");
                canvasVariable.ActualValue = finalCanvas;
                return finalCanvas;

            case Compound compound:
                var eval1 = EvaluateStatement(compound.Statement1, compound.Scope ?? scope);
                var eval2 = EvaluateStatement(compound.Statement2, compound.Scope ?? scope);
                return eval1 ?? eval2;

            // Currently allows infinite loops.
            case For @for:
                EvaluateStatement(@for.Declaration, @for.Scope ?? scope);
                var condition = EvaluateExpression(@for.Condition, @for.Scope ?? scope);
                while ((bool)condition)
                {
                    var eval = EvaluateStatement(@for.Statements, @for.Scope ?? scope);
                    if(eval != null) return eval;
                    EvaluateStatement(@for.Increment, @for.Scope ?? scope);
                    condition = EvaluateExpression(@for.Condition, @for.Scope ?? scope);
                }
                return null;

            // Currently allows infinite loops.
            case While @while:
                var whileCondition = EvaluateExpression(@while.Condition, @while.Scope ?? scope);
                while ((bool) whileCondition)
                {
                    var eval = EvaluateStatement(@while.Statements, @while.Scope ?? scope);
                    if (eval != null) return eval;
                    whileCondition = EvaluateExpression(@while.Condition, @while.Scope ?? scope);
                }
                return null;
            case FunctionDeclaration functionDeclaration:
                return null;
            case Declaration declaration:
                var val = EvaluateExpression(declaration.Expression, declaration.Scope ?? scope);
                var declIdentifier = declaration.Identifier.Name;
                var variable = scope.vTable.LookUp(declIdentifier);
                if (variable == null)
                {
                    errors.Add($"Variable {declIdentifier} not found");
                    return null;
                }

                variable.ActualValue = val;
                return null;

            case CollectionDeclaration collectionDeclaration:
                var collectionDeclarationVal = EvaluateExpression(collectionDeclaration.Expression, collectionDeclaration.Scope ?? scope);
                var collectionDeclIdentifier = collectionDeclaration.Identifier.Name;
                var collectionVariable = scope.vTable.LookUp(collectionDeclIdentifier);
                if (collectionVariable == null)
                {
                    errors.Add($"Variable {collectionDeclIdentifier} not found");
                    return null;
                }
                collectionVariable.ActualValue = collectionDeclarationVal;
                return null;

            case FunctionCallStatement functionCallStatement:
                var identifierAndFunction =
                    scope.LookupMethod(functionCallStatement.Identifier, scope, scope, new List<string>());
                string identifier = identifierAndFunction.Item1.Name;
                Function function = identifierAndFunction.Item2;
                if (function == null)
                {
                    errors.Add($"Function {identifier} not found");
                }

                var functionCallScope = functionCallStatement.Scope ?? scope;
                var functionScope = function.Scope;
                for (int i = 0; i < function.Parameters.Count; i++)
                {
                    var parameter = function.Parameters[i];
                    var functionCallVal = EvaluateExpression(functionCallStatement.Arguments[i], functionCallScope);
                    var var = functionScope.vTable.LocalLookUp(parameter.Identifier);
                    if (var != null)
                    {
                        var.ActualValue = functionCallVal;
                    }
                    else
                    {
                        functionScope.vTable.Bind(parameter.Identifier,
                            new Variable(parameter.Identifier, functionCallVal));
                    }
                }

                EvaluateStatement(function.Statements, functionScope);
                return null;

            case Assignment assignment:
                var assignExpression = EvaluateExpression(assignment.Expression, assignment.Scope ?? scope);
                var assignIdentifier = assignment.Identifier.Name;
                var assignVariable = scope.vTable.LookUp(assignIdentifier);
                if (assignVariable == null)
                {
                    errors.Add($"Variable {assignIdentifier} not found");
                    return null;
                }

                switch (assignment.Operator)
                {
                    case "+=":
                        assignVariable.ActualValue = (float) assignVariable.ActualValue! + (float) assignExpression;
                        break;
                    case "-=":
                        assignVariable.ActualValue = (float) assignVariable.ActualValue! - (float) assignExpression;
                        break;
                    case "*=":
                        assignVariable.ActualValue = (float) assignVariable.ActualValue! * (float) assignExpression;
                        break;
                    case "/=":
                        assignVariable.ActualValue = (float) assignVariable.ActualValue! / (float) assignExpression;
                        break;
                    case "=":
                        assignVariable.ActualValue = assignExpression;
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
                for (int i = 0; i < function.Parameters.Count; i++)
                {
                    var parameter = function.Parameters[i];
                    var functionCallVal = EvaluateExpression(functionCall.Arguments[i], functionCallScope);
                    var var = functionScope.vTable.LocalLookUp(parameter.Identifier);
                    if (var != null)
                    {
                        var.ActualValue = functionCallVal;
                    }
                    else
                    {
                        functionScope.vTable.Bind(parameter.Identifier,
                            new Variable(parameter.Identifier, functionCallVal));
                    }
                }
                var functionCallRes = EvaluateStatement(function.Statements, functionScope);
                return functionCallRes;

            case UnaryOp unaryOp:
                var unaryOpExpression = EvaluateExpression(unaryOp.Expression, scope);
                return unaryOp.Op switch
                {
                    "-" => -(float) unaryOpExpression,
                    "!" => !(bool) unaryOpExpression,
                    _ => throw new NotImplementedException()
                };

            case BinaryOp binaryOp:
                var left = EvaluateExpression(binaryOp.Left, scope);
                var right = EvaluateExpression(binaryOp.Right, scope);

                return binaryOp.Op switch
                {
                    "+" => binaryOp.Type switch
                    {
                        GasType.Num => (float)left + (float)right,
                        GasType.String => (string)left + (string)right,
                        _ => (float) left + (float) right
                    },
                    "-" => (float)left - (float)right,
                    "*" => (float)left * (float)right,
                    "/" => (float)left / (float)right,
                    "%" => (float)left % (float)right,
                    "<" => (float)left < (float)right,
                    ">" => (float)left > (float)right,
                    "<=" => (float)left <= (float)right,
                    ">=" => (float)left >= (float)right,
                    "!=" => left != right,
                    "==" => left == right,
                    "&&" => (bool)left && (bool)right,
                    "||" => (bool)left || (bool)right,
                    _ => throw new NotImplementedException()
                };

            case Identifier identifier:
                if (scope == null || scope.vTable == null || identifier == null || identifier.Name == null)
                {
                   errors.Add("Scope, VariableTable, Identifier or Identifier Name is null");
                }

                var variable = scope.LookupAttribute(identifier, scope, scope, new List<string>());

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
                var red = (float) EvaluateExpression(color.Red, scope);
                var green = (float) EvaluateExpression(color.Green, scope);
                var blue = (float) EvaluateExpression(color.Blue, scope);
                var alpha = (float) EvaluateExpression(color.Alpha, scope);
                return new FinalColor(red, green, blue, alpha);

            case Point point:
                var x = (float)EvaluateExpression(point.X, scope);
                var y = (float)EvaluateExpression(point.Y, scope);
                return new FinalPoint(x, y);

            case Square square:
                var topLeft = (FinalPoint) EvaluateExpression(square.TopLeft, scope);
                var length = (float) EvaluateExpression(square.Length, scope);
                var strokeSize = (float) EvaluateExpression(square.Stroke, scope);
                var squareFillColor = (FinalColor) EvaluateExpression(square.Color, scope);
                var squareStrokeColor =(FinalColor) EvaluateExpression(square.StrokeColor, scope);
                return new FinalSquare(topLeft, length, strokeSize, squareFillColor, squareStrokeColor);

            case Ellipse ellipse:
                var ellipseCentre = (FinalPoint) EvaluateExpression(ellipse.Center, scope);
                var ellipseRadiusX = (float) EvaluateExpression(ellipse.RadiusX, scope);
                var ellipseRadiusY = (float) EvaluateExpression(ellipse.RadiusY, scope);
                var ellipseStroke = (float) EvaluateExpression(ellipse.Stroke, scope);
                var ellipseFillColor = (FinalColor) EvaluateExpression(ellipse.Color, scope);
                var ellipseStrokeColor = (FinalColor) EvaluateExpression(ellipse.StrokeColor, scope);
                return new FinalEllipse(ellipseCentre, ellipseRadiusX, ellipseRadiusY, ellipseStroke, ellipseFillColor, ellipseStrokeColor);

            case Text text:
                var value = (string) EvaluateExpression(text.Value, scope);
                var position = (FinalPoint) EvaluateExpression(text.Position, scope);
                var font = (string) EvaluateExpression(text.Font, scope);
                var fontSize = (float) EvaluateExpression(text.FontSize, scope);
                var textColor = (FinalColor) EvaluateExpression(text.Color, scope);
                return new FinalText(value, position, font, fontSize, textColor);

            case Circle circle:
                var centre = (FinalPoint) EvaluateExpression(circle.Center, scope);
                var radius = (float) EvaluateExpression(circle.Radius, scope);
                var stroke = (float) EvaluateExpression(circle.Stroke, scope);
                var fillColor = (FinalColor) EvaluateExpression(circle.Color, scope);
                var strokeColor = (FinalColor) EvaluateExpression(circle.StrokeColor, scope);
                return new FinalCircle(centre, radius, stroke, fillColor, strokeColor);

            case Rectangle rectangle:
                var rectTopLeft = (FinalPoint) EvaluateExpression(rectangle.TopLeft, scope);
                var rectBottomRight = (FinalPoint) EvaluateExpression(rectangle.BottomRight, scope);
                var rectStroke = (float) EvaluateExpression(rectangle.Stroke, scope);
                var rectFillColor = (FinalColor) EvaluateExpression(rectangle.Color, scope);
                var rectStrokeColor = (FinalColor) EvaluateExpression(rectangle.StrokeColor, scope);
                return new FinalRectangle(rectTopLeft, rectBottomRight, rectStroke, rectFillColor, rectStrokeColor);

            case Line line:
                var lineIntercept = (float) EvaluateExpression(line.Intercept, scope);
                var lineStart = new FinalPoint(0, lineIntercept);
                var lineGradient = (float) EvaluateExpression(line.Gradient, scope);

                float lineEndX = lineGradient < 0 ? canvasWidth - Math.Abs((canvasHeight - lineIntercept) / lineGradient) + 1
                    : Math.Abs((canvasHeight - lineIntercept) / lineGradient) + 1;
                float lineEndY = lineGradient * lineEndX + lineIntercept;
                var lineEnd = new FinalPoint(lineEndX,lineEndY);

                var lineStroke = (float) EvaluateExpression(line.Stroke, scope);
                var lineColor = (FinalColor) EvaluateExpression(line.Color, scope);
                return new FinalLine(lineStart, lineEnd, lineStroke, lineColor);

            case SegLine segLine:
                var segLineStart = (FinalPoint) EvaluateExpression(segLine.Start, scope);
                var segLineEnd = (FinalPoint) EvaluateExpression(segLine.End, scope);
                var segLineStroke = (float) EvaluateExpression(segLine.Stroke, scope);
                var segLineColor = (FinalColor) EvaluateExpression(segLine.Color, scope);
                return new FinalSegLine(segLineStart, segLineEnd, segLineStroke, segLineColor);

            case Polygon polygon:
                var polygonPoints = (FinalList) EvaluateExpression(polygon.Points, scope);
                var polygonColor = (FinalColor) EvaluateExpression(polygon.Color, scope);
                var polygonStroke = (float) EvaluateExpression(polygon.Stroke, scope);
                var polygonStrokeColor = (FinalColor) EvaluateExpression(polygon.StrokeColor, scope);
                return new FinalPolygon(polygonPoints, polygonStroke, polygonColor, polygonStrokeColor);

            case Group group:
                var finalPoint = (FinalPoint) EvaluateExpression(group.Point, scope);
                EvaluateStatement(group.Statements, group.Scope ?? scope);
                return new FinalGroup(finalPoint, group.Scope ?? scope);

            case AddToList addToList:
                var listVariable = scope.vTable.LookUp(addToList.ListIdentifier.Name);

                if (listVariable == null)
                {
                    errors.Add($"Variable {addToList.ListIdentifier.Name} not found");
                    return null;
                }

                if (listVariable.ActualValue == null)
                {
                    listVariable.ActualValue = new FinalList(new List<object>(), scope);
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
                var listToRemoveFrom = scope.vTable.LookUp(removeFromList.ListIdentifier.Name);
                var indexToRemove = Convert.ToInt32(EvaluateExpression(removeFromList.Index, removeFromList.Scope ?? scope));

                if (listToRemoveFrom == null) throw new Exception($"Variable {removeFromList.ListIdentifier.Name} not found");
                if (listToRemoveFrom.ActualValue == null) throw new Exception($"Variable {removeFromList.ListIdentifier.Name} is already empty");
                if (listToRemoveFrom.ActualValue is not FinalList destinedList) throw new Exception($"Variable {removeFromList.ListIdentifier.Name} is not a list");
                if (indexToRemove < 0 || indexToRemove >= destinedList.Values.Count) throw new Exception($"Index {indexToRemove} out of range for list {removeFromList.ListIdentifier.Name}");

                destinedList.Values.RemoveAt(indexToRemove);
                return null;


            case GetFromList getFromList:
                var listToGetFrom = scope.vTable.LookUp(getFromList.ListIdentifier.Name);
                var indexOfValue = Convert.ToInt32(EvaluateExpression(getFromList.Index, getFromList.Scope ?? scope));

                if (listToGetFrom == null) throw new Exception($"Variable {getFromList.ListIdentifier.Name} not found");
                if (listToGetFrom.ActualValue is not FinalList sourceList) throw new Exception($"Variable {getFromList.ListIdentifier.Name} is not a list");
                if (indexOfValue < 0 || indexOfValue >= sourceList.Values.Count) throw new Exception($"Index {indexOfValue} out of range for list {getFromList.ListIdentifier.Name}");

                var valueToGet = sourceList.Values[indexOfValue];

                return valueToGet;

            case LengthOfList lengthOfList:
                var listToCheck = scope.vTable.LookUp(lengthOfList.ListIdentifier.Name);

                if (listToCheck == null) throw new Exception($"Variable {lengthOfList.ListIdentifier.Name} not found");
                if (listToCheck.ActualValue is not FinalList listToCheckLength) throw new Exception($"Variable {lengthOfList.ListIdentifier.Name} is not a list");

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
