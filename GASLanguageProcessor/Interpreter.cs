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

    public object? EvaluateStatement(Statement statement, VarEnv varEnv, FuncEnv funcEnv, Store store) {
        switch (statement)
        {
            case Canvas canvas:
                canvasWidth = (float)EvaluateExpression(canvas.Width, varEnv, funcEnv, store);
                canvasHeight = (float)EvaluateExpression(canvas.Height, varEnv, funcEnv, store);
                var backgroundColor = (FinalColor) EvaluateExpression(canvas.BackgroundColor, varEnv, funcEnv, store);
                var finalCanvas = new FinalCanvas(canvasWidth, canvasHeight, backgroundColor);

                int next = varEnv.GetNext();
                varEnv.Bind("canvas",  next);
                store.Bind(next, finalCanvas);
                return null;

            case Compound compound:
                var eval1 = EvaluateStatement(compound.Statement1, varEnv, funcEnv, store);
                if (eval1 != null)
                {
                    return eval1;
                }

                var eval2 = EvaluateStatement(compound.Statement2, varEnv, funcEnv, store);
                return eval2;

            // Currently allows infinite loops.
            case For @for:
                EvaluateStatement(@for.Initializer, varEnv, funcEnv, store);
                var condition = EvaluateExpression(@for.Condition, varEnv, funcEnv, store);
                while ((bool)condition)
                {
                    var eval = EvaluateStatement(@for.Statements, varEnv, funcEnv, store);
                    if (eval != null)
                    {
                        return eval;
                    }
                    EvaluateStatement(@for.Incrementer, varEnv, funcEnv, store);
                    condition = EvaluateExpression(@for.Condition, varEnv, funcEnv, store);
                }

                return null;

            // Currently allows infinite loops.
            case While @while:
                var whileCondition = EvaluateExpression(@while.Condition, varEnv, funcEnv, store);

                varEnv = varEnv.EnterScope();
                funcEnv = funcEnv.EnterScope();

                while ((bool)whileCondition)
                {
                    var eval = EvaluateStatement(@while.Statements, varEnv, funcEnv, store);
                    if (eval != null) return eval;
                    whileCondition = EvaluateExpression(@while.Condition, varEnv.Parent, funcEnv.Parent, store);
                }
                return null;

            case If @if:
                var ifCondition = EvaluateExpression(@if.Condition, varEnv, funcEnv, store);

                varEnv = varEnv.EnterScope();
                funcEnv = funcEnv.EnterScope();

                if ((bool)ifCondition)
                {
                    return EvaluateStatement(@if.Statements, varEnv, funcEnv, store);
                }

                if (@if.Else != null)
                {
                    return EvaluateStatement(@if.Else, varEnv, funcEnv, store);
                }

                return null;
            case FunctionDeclaration functionDeclaration:
                var parameters = functionDeclaration.Parameters.Select(x => x.Identifier.Name).ToList();
                var statements = functionDeclaration.Statements;
                var functionDecl = new Function(parameters, statements, new VarEnv(varEnv), new FuncEnv(funcEnv), store);
                funcEnv.Bind(functionDeclaration.Identifier.Name, functionDecl);
                return null;
            case Declaration declaration:
                var val = EvaluateExpression(declaration.Expression, varEnv, funcEnv, store);
                var declIdentifier = declaration.Identifier.Name;
                next = varEnv.GetNext();
                varEnv.Bind(declIdentifier, next);
                store.Bind(next, val);
                return null;

            case FunctionCallStatement functionCall:
                var function = funcEnv.LookUp(functionCall.Identifier.Name);
                if (function == null)
                {
                    errors.Add($"Function {functionCall.Identifier.Name} not found in the FunctionTable");
                    return null;
                }

                if (function.Parameters.Count != functionCall.Arguments.Count)
                {
                    errors.Add($"Function {functionCall.Identifier.Name} has {function.Parameters.Count} parameters, but {functionCall.Arguments.Count} arguments were provided");
                    return null;
                }

                for (int i = 0; i < function.Parameters.Count; i++)
                {
                    var parameter = function.Parameters[i];
                    var functionCallVal = EvaluateExpression(functionCall.Arguments[i], varEnv, funcEnv, store);
                    var varIndex = function.VarEnv.LocalLookUp(parameter);
                    if (varIndex == null)
                    {
                        next = varEnv.GetNext();
                        function.VarEnv.Bind(parameter, next);
                        function.Store.Bind(next, functionCallVal);
                    }
                    else
                    {
                        function.Store.Bind(varIndex.Value, functionCallVal);
                    }
                }

                EvaluateStatement(function.Statements, function.VarEnv, function.FuncEnv, function.Store);

                return null;

            case Assignment assignment:
                var assignExpression = EvaluateExpression(assignment.Expression, varEnv, funcEnv, store);
                var assignIdentifier = assignment.Identifier.Name;
                var assignIndex = varEnv.LookUp(assignIdentifier);

                if (assignIndex == null)
                {
                    errors.Add($"Variable {assignIdentifier} not found");
                    return null;
                }
                var assignVariable = store.LookUp(assignIndex.Value);


                switch (assignment.Operator)
                {
                    case "+=":
                        assignVariable = (float)assignVariable! + (float)assignExpression;
                        break;
                    case "-=":
                        assignVariable = (float)assignVariable! - (float)assignExpression;
                        break;
                    case "*=":
                        assignVariable = (float)assignVariable! * (float)assignExpression;
                        break;
                    case "/=":
                        assignVariable = (float)assignVariable! / (float)assignExpression;
                        break;
                    case "=":
                        assignVariable = assignExpression;
                        break;
                }

                store.Bind(assignIndex.Value, assignVariable);

                return null;

            case Increment increment:
                var incrementIdentifier = increment.Identifier.Name;
                var incrementVariableIndex = varEnv.LookUp(incrementIdentifier);
                if (incrementVariableIndex == null)
                {
                    errors.Add($"Variable {incrementIdentifier} not found in the VariableTable");
                    return null;
                }
                var incrementVariable = store.LookUp(incrementVariableIndex.Value);
                var op = increment.Operator;

                switch (op)
                {
                    case "++":
                        incrementVariable = (float)incrementVariable! + 1;
                        break;
                    case "--":
                        incrementVariable = (float)incrementVariable! - 1;
                        break;
                }

                store.Bind(incrementVariableIndex.Value, incrementVariable);

                return null;

            case Return returnStatement:
                return EvaluateExpression(returnStatement.Expression, varEnv, funcEnv, store);
        }

        return null;
    }

    public object EvaluateExpression(Expression expression, VarEnv varEnv, FuncEnv funcEnv, Store store)
    {
        switch (expression)
        {
            case FunctionCallTerm functionCall:
                var function = funcEnv.LookUp(functionCall.Identifier.Name);
                if (function == null)
                {
                    errors.Add($"Function {functionCall.Identifier.Name} not found in the FunctionTable");
                    return null;
                }

                if (function.Parameters.Count != functionCall.Arguments.Count)
                {
                    errors.Add($"Function {functionCall.Identifier.Name} has {function.Parameters.Count} parameters, but {functionCall.Arguments.Count} arguments were provided");
                    return null;
                }

                for (int i = 0; i < function.Parameters.Count; i++)
                {
                    var parameter = function.Parameters[i];
                    var functionCallVal = EvaluateExpression(functionCall.Arguments[i], varEnv, funcEnv, store);
                    var varIndex = function.VarEnv.LocalLookUp(parameter);
                    if (varIndex == null)
                    {
                        int next = varEnv.GetNext();
                        function.VarEnv.Bind(parameter, next);
                        function.Store.Bind(next, functionCallVal);
                    }
                    else
                    {
                        function.Store.Bind(varIndex.Value, functionCallVal);
                    }
                }

                var functionCallRes = EvaluateStatement(function.Statements, function.VarEnv, function.FuncEnv, function.Store);

                if (functionCallRes == null)
                {
                    throw new Exception($"Function {functionCall.Identifier.Name} did not return a value");
                }

                return functionCallRes;

            case UnaryOp unaryOp:
                var unaryOpExpression = EvaluateExpression(unaryOp.Expression, varEnv, funcEnv, store);
                return unaryOp.Op switch
                {
                    "-" => -(float)unaryOpExpression,
                    "!" => !(bool)unaryOpExpression,
                    _ => throw new NotImplementedException()
                };

            case BinaryOp binaryOp:
                var left = EvaluateExpression(binaryOp.Left, varEnv, funcEnv, store);
                var right = EvaluateExpression(binaryOp.Right, varEnv, funcEnv, store);

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
                var variableIndex = varEnv.LookUp(identifier.Name);

                if (variableIndex == null)
                {
                    errors.Add($"Variable {identifier.Name} not found in the VariableTable");
                    return null;
                }
                var variable = store.LookUp(variableIndex.Value);

                if (variable == null)
                {
                    errors.Add($"Variable {identifier.Name} not found in the Store");
                    return null;
                }

                return variable;

            case Reference reference:
                var referenceIndex = varEnv.LookUp(reference.Identifier.Name);

                if (referenceIndex == null)
                {
                    errors.Add($"Variable {reference.Identifier.Name} not found in the VariableTable");
                    return null;
                }

                return referenceIndex.Value;

            case Num num: // Num is a float; CultureInfo is used to ensure that the decimal separator is a dot
                return float.Parse(num.Value, CultureInfo.InvariantCulture);

            case Boolean boolean:
                return bool.Parse(boolean.Value);

            case String stringTerm:
                return stringTerm.Value.TrimStart('"').TrimEnd('"').Replace('\\', ' ');

            case Color color:
                var red = (float)EvaluateExpression(color.Red, varEnv, funcEnv, store);
                var green = (float)EvaluateExpression(color.Green, varEnv, funcEnv, store);
                var blue = (float)EvaluateExpression(color.Blue, varEnv, funcEnv, store);
                var alpha = (float)EvaluateExpression(color.Alpha, varEnv, funcEnv, store);

                return new FinalColor(red, green, blue, alpha);

            case Point point:
                var x = (float)EvaluateExpression(point.X, varEnv, funcEnv, store);
                var y = (float)EvaluateExpression(point.Y, varEnv, funcEnv, store);
                return new FinalPoint(x, y);

            case Square square:
                var topLeft = (FinalPoint)EvaluateExpression(square.TopLeft, varEnv, funcEnv, store);
                var length = (float)EvaluateExpression(square.Length, varEnv, funcEnv, store);
                var strokeSize = (float)EvaluateExpression(square.Stroke, varEnv, funcEnv, store);
                var squareFillColor = (FinalColor)EvaluateExpression(square.Color, varEnv, funcEnv, store);
                var squareStrokeColor = (FinalColor)EvaluateExpression(square.StrokeColor, varEnv, funcEnv, store);
                var cornerRounding = (float)EvaluateExpression(square.CornerRounding, varEnv, funcEnv, store);
                return new FinalSquare(topLeft, length, strokeSize, squareFillColor, squareStrokeColor, cornerRounding);

            case Ellipse ellipse:
                var ellipseCentre = (FinalPoint)EvaluateExpression(ellipse.Center, varEnv, funcEnv, store);
                var ellipseRadiusX = (float)EvaluateExpression(ellipse.RadiusX, varEnv, funcEnv, store);
                var ellipseRadiusY = (float)EvaluateExpression(ellipse.RadiusY, varEnv, funcEnv, store);
                var ellipseStroke = (float)EvaluateExpression(ellipse.Stroke, varEnv, funcEnv, store);
                var ellipseFillColor = (FinalColor)EvaluateExpression(ellipse.Color, varEnv, funcEnv, store);
                var ellipseStrokeColor = (FinalColor)EvaluateExpression(ellipse.StrokeColor, varEnv, funcEnv, store);
                return new FinalEllipse(ellipseCentre, ellipseRadiusX, ellipseRadiusY, ellipseStroke, ellipseFillColor,
                    ellipseStrokeColor);

            case Text text:
                var value = (string)EvaluateExpression(text.Value, varEnv, funcEnv, store);
                var position = (FinalPoint)EvaluateExpression(text.Position, varEnv, funcEnv, store);
                var font = (string)EvaluateExpression(text.Font, varEnv, funcEnv, store);
                var fontSize = (float)EvaluateExpression(text.FontSize, varEnv, funcEnv, store);
                var fontWeight = (float)EvaluateExpression(text.FontWeight, varEnv, funcEnv, store);
                var textColor = (FinalColor)EvaluateExpression(text.Color, varEnv, funcEnv, store);
                return new FinalText(value, position, font, fontSize, fontWeight, textColor);

            case Circle circle:
                var centre = (FinalPoint)EvaluateExpression(circle.Center, varEnv, funcEnv, store);
                var radius = (float)EvaluateExpression(circle.Radius, varEnv, funcEnv, store);
                var stroke = (float)EvaluateExpression(circle.Stroke, varEnv, funcEnv, store);
                var fillColor = (FinalColor)EvaluateExpression(circle.Color, varEnv, funcEnv, store);
                var strokeColor = (FinalColor)EvaluateExpression(circle.StrokeColor, varEnv, funcEnv, store);
                return new FinalCircle(centre, radius, stroke, fillColor, strokeColor);

            case Rectangle rectangle:
                var rectTopLeft = (FinalPoint)EvaluateExpression(rectangle.TopLeft, varEnv, funcEnv, store);
                var rectBottomRight = (FinalPoint)EvaluateExpression(rectangle.BottomRight, varEnv, funcEnv, store);
                var rectStroke = (float)EvaluateExpression(rectangle.Stroke, varEnv, funcEnv, store);
                var rectFillColor = (FinalColor)EvaluateExpression(rectangle.Color, varEnv, funcEnv, store);
                var rectStrokeColor = (FinalColor)EvaluateExpression(rectangle.StrokeColor, varEnv, funcEnv, store);
                var rectCornerRounding = (float)EvaluateExpression(rectangle.CornerRounding, varEnv, funcEnv, store);
                return new FinalRectangle(rectTopLeft, rectBottomRight, rectStroke, rectFillColor, rectStrokeColor, rectCornerRounding);

            case Line line:
                var lineIntercept = (float)EvaluateExpression(line.Intercept, varEnv, funcEnv, store);
                var lineGradient = (float)EvaluateExpression(line.Gradient, varEnv, funcEnv, store);
                var lineStart = new FinalPoint(-1, lineIntercept-lineGradient);

                float lineEndX = lineGradient < 0
                    ? canvasWidth - Math.Abs((canvasHeight - lineIntercept) / lineGradient) + 1
                    : Math.Abs((canvasHeight - lineIntercept) / lineGradient) + 1;
                float lineEndY = lineGradient * lineEndX + lineIntercept;
                var lineEnd = new FinalPoint(lineEndX, lineEndY);

                var lineStroke = (float)EvaluateExpression(line.Stroke, varEnv, funcEnv, store);
                var lineColor = (FinalColor)EvaluateExpression(line.Color, varEnv, funcEnv, store);
                return new FinalLine(lineStart, lineEnd, lineStroke, lineColor);

            case SegLine segLine:
                var segLineStart = (FinalPoint)EvaluateExpression(segLine.Start, varEnv, funcEnv, store);
                var segLineEnd = (FinalPoint)EvaluateExpression(segLine.End, varEnv, funcEnv, store);
                var segLineStroke = (float)EvaluateExpression(segLine.Stroke, varEnv, funcEnv, store);
                var segLineColor = (FinalColor)EvaluateExpression(segLine.Color, varEnv, funcEnv, store);
                return new FinalSegLine(segLineStart, segLineEnd, segLineStroke, segLineColor);

            case Arrow arrow:
                var arrowStart = (FinalPoint)EvaluateExpression(arrow.Start, varEnv, funcEnv, store);
                var arrowEnd = (FinalPoint)EvaluateExpression(arrow.End, varEnv, funcEnv, store);
                var arrowStroke = (float)EvaluateExpression(arrow.Stroke, varEnv, funcEnv, store);
                var arrowColor = (FinalColor)EvaluateExpression(arrow.Color, varEnv, funcEnv, store);
                return new FinalArrow(arrowStart, arrowEnd, arrowStroke, arrowColor);

            case Polygon polygon:
                var polygonPoints = (FinalList)EvaluateExpression(polygon.Points, varEnv, funcEnv, store);
                var polygonColor = (FinalColor)EvaluateExpression(polygon.Color, varEnv, funcEnv, store);
                var polygonStroke = (float)EvaluateExpression(polygon.Stroke, varEnv, funcEnv, store);
                var polygonStrokeColor = (FinalColor)EvaluateExpression(polygon.StrokeColor, varEnv, funcEnv, store);
                return new FinalPolygon(polygonPoints, polygonStroke, polygonColor, polygonStrokeColor);

            case Group group:
                var finalPoint = (FinalPoint)EvaluateExpression(group.Point, varEnv, funcEnv, store);
                varEnv = varEnv.EnterScope();
                funcEnv = funcEnv.EnterScope();

                EvaluateStatement(group.Statements, varEnv, funcEnv, store);
                return new FinalGroup(finalPoint, varEnv);

            case AddToList addToList:
                var listVariableIndex = varEnv.LookUp(addToList.ListIdentifier.Name);

                if (listVariableIndex == null)
                {
                    errors.Add($"Variable {addToList.ListIdentifier.Name} not found in the VariableTable");
                    return null;
                }

                var listVariable = store.LookUp(listVariableIndex.Value);

                if (listVariable == null)
                {
                    errors.Add($"Variable {addToList.ListIdentifier.Name} not found in the Store");
                    return null;
                }

                if(listVariable is not FinalList destinedList1)
                {
                    errors.Add($"Variable {addToList.ListIdentifier.Name} is not a list");
                    return null;
                }

                var valueToAdd = EvaluateExpression(addToList.Value, varEnv, funcEnv, store);
                destinedList1?.Values.Add(valueToAdd);
                return null;

            case RemoveFromList removeFromList:
                var listToRemoveFromIndex = varEnv.LookUp(removeFromList.ListIdentifier.Name);

                if (listToRemoveFromIndex == null)
                {
                    errors.Add($"Variable {removeFromList.ListIdentifier.Name} not found in the VariableTable");
                    return null;
                }

                var listToRemoveFrom = store.LookUp(listToRemoveFromIndex.Value);

                if (listToRemoveFrom == null)
                {
                    errors.Add($"Variable {removeFromList.ListIdentifier.Name} not found in the Store");
                    return null;
                }

                if (listToRemoveFrom is not FinalList destinedList)
                {
                    errors.Add($"Variable {removeFromList.ListIdentifier.Name} is not a list");
                    return null;
                }

                var indexToRemove = Convert.ToInt32(EvaluateExpression(removeFromList.Index, varEnv, funcEnv, store));

                if (indexToRemove < 0 || indexToRemove >= destinedList.Values.Count)
                {
                    errors.Add($"Index {indexToRemove} out of range for list {removeFromList.ListIdentifier.Name}");
                    return null;
                }

                destinedList.Values.RemoveAt(indexToRemove);
                return null;


            case GetFromList getFromList:
                var listToGetFromIndex = varEnv.LookUp(getFromList.ListIdentifier.Name);

                if (listToGetFromIndex == null)
                {
                    errors.Add($"Variable {getFromList.ListIdentifier.Name} not found");
                    return null;
                }

                var listToGetFrom = store.LookUp(listToGetFromIndex.Value);

                if (listToGetFrom == null)
                {
                    errors.Add($"Variable {getFromList.ListIdentifier.Name} not found in the Store");
                    return null;
                }

                if (listToGetFrom is not FinalList sourceList)
                {
                    errors.Add($"Variable {getFromList.ListIdentifier.Name} is not a list");
                    return null;
                }

                var indexOfValue = Convert.ToInt32(EvaluateExpression(getFromList.Index, varEnv, funcEnv, store));

                if (indexOfValue < 0 || indexOfValue >= sourceList.Values.Count)
                {
                    errors.Add($"Index {indexOfValue} out of range for list {getFromList.ListIdentifier.Name}");
                    return null;
                }

                var valueToGet = sourceList.Values[indexOfValue];

                return valueToGet;

            case LengthOfList lengthOfList:
                var listToCheckIndex = varEnv.LookUp(lengthOfList.ListIdentifier.Name);

                if (listToCheckIndex == null)
                {
                    errors.Add($"Variable {lengthOfList.ListIdentifier.Name} not found");
                    return null;
                }

                var listToCheck = store.LookUp(listToCheckIndex.Value);

                if (listToCheck == null)
                {
                    errors.Add($"Variable {lengthOfList.ListIdentifier.Name} not found");
                    return null;
                }

                if (listToCheck is not FinalList listToCheckLength)
                {
                    errors.Add($"Variable {lengthOfList.ListIdentifier.Name} is not a list");
                    return null;
                }

                return (float)listToCheckLength.Values.Count;

            case List list:
                var values = new List<object>();
                foreach (var expr in list.Expressions)
                {
                    values.Add(EvaluateExpression(expr, varEnv, funcEnv, store));
                }

                return new FinalList(values);
        }

        return null;
    }
}