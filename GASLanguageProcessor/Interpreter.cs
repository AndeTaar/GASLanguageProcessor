using System.Globalization;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using Expression = GASLanguageProcessor.AST.Expressions.Expression;
using Number = GASLanguageProcessor.AST.Expressions.Terms.Number;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;

namespace GASLanguageProcessor;

public class Interpreter
{
    public float canvasWidth;
    public float canvasHeight;

    public object EvaluateStatement(Statement statement, Scope scope)
    {
        switch (statement)
        {
            case Canvas canvas:
                var width = (float)EvaluateExpression(canvas.Width, scope);
                var height = (float)EvaluateExpression(canvas.Height, scope);
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
                EvaluateStatement(compound.Statement1, compound.Scope ?? scope);
                EvaluateStatement(compound.Statement2, compound.Scope ?? scope);
                return null;

            // Currently allows infinite loops.
            case For @for:
                EvaluateStatement(@for.Declaration, @for.Scope ?? scope);
                var condition = EvaluateExpression(@for.Condition, @for.Scope ?? scope);
                while ((bool)condition)
                {
                    EvaluateStatement(@for.Statements, @for.Scope ?? scope);
                    EvaluateStatement(@for.Increment, @for.Scope ?? scope);
                    condition = EvaluateExpression(@for.Condition, @for.Scope ?? scope);
                }

                return null;

            // Currently allows infinite loops.
            case While @while:
                var whileCondition = EvaluateExpression(@while.Condition, @while.Scope ?? scope);
                while ((bool) whileCondition)
                {
                    EvaluateStatement(@while.Statements, @while.Scope ?? scope);
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
                    Console.WriteLine($"Variable {declIdentifier} not found");
                    return null;
                }

                variable.ActualValue = val;
                return val;

            case CollectionDeclaration collectionDeclaration:
                var collectionDeclarationVal = EvaluateExpression(collectionDeclaration.Expression, collectionDeclaration.Scope ?? scope);
                var collectionDeclIdentifier = collectionDeclaration.Identifier.Name;
                var collectionVariable = scope.vTable.LookUp(collectionDeclIdentifier);
                if (collectionVariable == null)
                {
                    Console.WriteLine($"Variable {collectionDeclIdentifier} not found");
                    return null;
                }
                collectionVariable.ActualValue = collectionDeclarationVal;
                return collectionDeclarationVal;

            case FunctionCallStatement functionCallStatement:
                var identifierAndFunction =
                    scope.LookupMethod(functionCallStatement.Identifier, scope, scope, new List<string>());
                string identifier = identifierAndFunction.Item1.Name;
                Function function = identifierAndFunction.Item2;
                if (function == null)
                {
                    throw new Exception($"Function {identifier} not found");
                }

                var functionCallScope = functionCallStatement.Scope ?? scope;
                var functionScope = function.Scope;
                functionScope.vTable.Variables.Clear();
                for (int i = 0; i < function.Parameters.Count; i++)
                {
                    var parameter = function.Parameters[i];
                    var functionCallVal = EvaluateExpression(functionCallStatement.Arguments[i], functionCallScope);
                    functionScope.vTable.Bind(parameter.Identifier,
                        new Variable(parameter.Identifier, functionCallVal));
                }

                var functionCallRes = EvaluateStatement(function.Statements, functionScope);
                return functionCallRes;

            case Assignment assignment:
                var assignExpression = EvaluateExpression(assignment.Expression, assignment.Scope ?? scope);
                var assignIdentifier = assignment.Identifier.Name;
                var assignVariable = scope.vTable.LookUp(assignIdentifier);
                if (assignVariable == null)
                {
                    Console.WriteLine($"Variable {assignIdentifier} not found");
                    return null;
                }

                assignVariable.ActualValue = assignExpression;
                return assignExpression;

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
                    throw new Exception($"Function {functionCall.Identifier.Name} not found");
                }

                var functionCallScope = functionCall.Scope ?? scope;
                var functionScope = function.Scope;
                functionScope.vTable.Variables.Clear();
                for (int i = 0; i < function.Parameters.Count; i++)
                {
                    var parameter = function.Parameters[i];
                    var val = EvaluateExpression(functionCall.Arguments[i], functionCallScope);
                    functionScope.vTable.Bind(parameter.Identifier, new Variable(parameter.Identifier, val));
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
                        GasType.Number => (float)left + (float)right,
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
                    throw new Exception("Scope, VariableTable, Identifier or Identifier Name is null");
                }

                var variable = scope.LookupAttribute(identifier, scope, scope, new List<string>());

                if (variable == null)
                {
                    throw new Exception($"Variable {identifier.Name} not found in the VariableTable");
                }

                if (variable.ActualValue != null)
                {
                    return variable.ActualValue;
                }
                return EvaluateExpression(variable.FormalValue, scope);

            case Number number: // Number is a float; CultureInfo is used to ensure that the decimal separator is a dot
                return float.Parse(number.Value, CultureInfo.InvariantCulture);

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

            case Group group:
                var finalPoint = (FinalPoint) EvaluateExpression(group.Point, scope);
                EvaluateStatement(group.Statements, group.Scope ?? scope);
                return new FinalGroup(finalPoint, group.Scope ?? scope);

            case AddToList addToList:
                var listToAddTo = scope.vTable.LookUp(addToList.ListIdentifier.Name);

                if (listToAddTo == null) throw new Exception($"Variable {addToList.ListIdentifier.Name} not found");
                if (listToAddTo.ActualValue == null)
                {
                    listToAddTo.ActualValue = new FinalList(new List<object>(), scope);
                }
                if (listToAddTo.ActualValue is not FinalList destList) throw new Exception($"Variable {addToList.ListIdentifier.Name} is not a list");

                var valueToAdd = EvaluateExpression(addToList.Value, addToList.Scope ?? scope);
                destList.Values.Add(valueToAdd);
                return null;
            
            case GetFromList getFromList:
                var listToGetFrom = scope.vTable.LookUp(getFromList.ListIdentifier.Name);
                
                if (listToGetFrom == null) throw new Exception($"Variable {getFromList.ListIdentifier.Name} not found");
                //if (listToGetFrom.ActualValue == null) throw new Exception($"Variable {getFromList.ListIdentifier.Name} does not contain a value at index {getFromList.Index}");
                if (listToGetFrom.ActualValue is not FinalList sourceList) throw new Exception($"Variable {getFromList.ListIdentifier.Name} is not a list");

                var indexOfValue = Convert.ToInt32(EvaluateExpression(getFromList.Index, getFromList.Scope ?? scope));
                if (indexOfValue < 0 || indexOfValue >= sourceList.Values.Count) throw new Exception($"Index {indexOfValue} out of range for list {getFromList.ListIdentifier.Name}");
                var valueToGet = sourceList.Values[indexOfValue];
                var typeOfList = listToGetFrom.Type;

                return valueToGet;
               /* return typeOfList switch
                {
                    GasType.Number => (float) valueToGet,
                    GasType.String => (string) valueToGet,
                    GasType.Boolean => (bool) valueToGet,
                    GasType.Circle => (FinalCircle) valueToGet,
                    GasType.Color => (FinalColor) valueToGet,
                    GasType.Ellipse => (FinalEllipse) valueToGet,
                    GasType.Line => (FinalLine) valueToGet,
                    GasType.Rectangle => (FinalRectangle) valueToGet,
                    GasType.Square => (FinalSquare) valueToGet,
                    GasType.SegLine => (FinalSegLine) valueToGet,
                    GasType.Text => (FinalText) valueToGet,
                    _ => valueToGet
                };*/

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
