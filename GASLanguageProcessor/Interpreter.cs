using System.Globalization;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using Expression = GASLanguageProcessor.AST.Expressions.Expression;
using Num = GASLanguageProcessor.AST.Expressions.Terms.Num;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;

namespace GASLanguageProcessor;

public class Interpreter
{
    public float canvasHeight;
    public float canvasWidth;
    public List<string> errors = new();

    public Store EvaluateProgram(AST.Expressions.Terms.Program program, VarEnv varEnv, FuncEnv funcEnv, RecEnv recEnv, Store store)
    {
        EvaluateStatement(program.Statements, varEnv, funcEnv, recEnv, store);

        return store;
    }

    public (object?, VarEnv varEnv, FuncEnv funcEnv, RecEnv recEnv, Store store) EvaluateStatement(Statement statement, VarEnv varEnv, FuncEnv funcEnv, RecEnv recEnv, Store store)
    {
        switch (statement)
        {
            case Canvas canvas:
                var val = EvaluateExpression(canvas.Width, varEnv, funcEnv, recEnv, store);
                canvasWidth = (float)val;
                val = EvaluateExpression(canvas.Height, varEnv, funcEnv, recEnv, store);
                canvasHeight = (float)val;
                var backgroundColor = (FinalColor)EvaluateExpression(canvas.BackgroundColor, varEnv, funcEnv, recEnv, store);
                var finalCanvas = new FinalCanvas(canvasWidth, canvasHeight, backgroundColor);

                var next = varEnv.GetNext();
                varEnv.Bind("canvas", next);
                store.Bind(next, finalCanvas);
                return (null, varEnv, funcEnv, recEnv, store);

            case RecordDefinition recordDefinition:
                varEnv = varEnv.EnterScope();
                funcEnv = funcEnv.EnterScope();
                recEnv = recEnv.EnterScope();
                recordDefinition.ConstructorDeclarations.ForEach(constructor =>
                    EvaluateConstructorDeclaration(constructor, varEnv, funcEnv, recEnv, store));

                recordDefinition.FunctionDeclaration.ForEach(func =>
                    EvaluateFunctionDeclaration(func, varEnv, funcEnv, recEnv, store));

                recEnv.TypeBind(recordDefinition.RecordType.Value, (funcEnv, recEnv));

                return (null, varEnv, funcEnv, recEnv, store);


            case Compound compound:
                var tuple = EvaluateStatement(compound.Statement1, varEnv, funcEnv, recEnv, store);
                if (tuple.Item1 != null) return tuple;

                tuple = EvaluateStatement(compound.Statement2, varEnv, funcEnv, recEnv, store);
                return tuple;

            // Currently allows infinite loops.
            case For @for:
                EvaluateStatement(@for.Initializer, varEnv, funcEnv, recEnv, store);
                val = EvaluateExpression(@for.Condition, varEnv, funcEnv, recEnv, store);
                while ((bool)val)
                {
                    tuple = EvaluateStatement(@for.Statements, varEnv, funcEnv, recEnv, store);
                    if (tuple.Item1 != null) return tuple;

                    EvaluateStatement(@for.Incrementer, varEnv, funcEnv, recEnv, store);
                    val = EvaluateExpression(@for.Condition, varEnv, funcEnv, recEnv, store);
                }

                return (null, varEnv, funcEnv, recEnv, store);

            // Currently allows infinite loops.
            case While @while:
                val = EvaluateExpression(@while.Condition, varEnv, funcEnv, recEnv, store);

                varEnv = varEnv.EnterScope();
                funcEnv = funcEnv.EnterScope();

                while ((bool)val)
                {
                    tuple = EvaluateStatement(@while.Statements, varEnv, funcEnv, recEnv, store);
                    if (tuple.Item1 != null) return tuple;
                    val = EvaluateExpression(@while.Condition, varEnv.Parent, funcEnv.Parent, recEnv, store);
                }

                return (null, varEnv, funcEnv, recEnv, store);

            case If @if:
                val = EvaluateExpression(@if.Condition, varEnv, funcEnv, recEnv, store);

                varEnv = varEnv.EnterScope();
                funcEnv = funcEnv.EnterScope();

                if ((bool)val) return EvaluateStatement(@if.Statements, varEnv, funcEnv, recEnv, store);

                if (@if.Else != null) return EvaluateStatement(@if.Else, varEnv, funcEnv, recEnv, store);

                return (null, varEnv, funcEnv, recEnv, store);

            case FunctionDeclaration functionDeclaration:
                var funcDecl = EvaluateFunctionDeclaration(functionDeclaration, varEnv, funcEnv, recEnv, store);
                return (null, funcDecl.Item1, funcDecl.Item2, recEnv, store);

            case FunctionCallStatement functionCall:
                var function = funcEnv.LookUp(functionCall.Identifier.Name);
                if (function == null)
                {
                    errors.Add($"Function {functionCall.Identifier.Name} not found in the FunctionTable");
                    return (null, varEnv, funcEnv, recEnv, store);
                }

                if (function.Parameters.Count != functionCall.Arguments.Count)
                {
                    errors.Add(
                        $"Function {functionCall.Identifier.Name} has {function.Parameters.Count} parameters, but {functionCall.Arguments.Count} arguments were provided");
                    return (null, varEnv, funcEnv, recEnv, store);
                }

                for (var i = 0; i < function.Parameters.Count; i++)
                {
                    var parameter = function.Parameters[i];
                    var functionCallVal = EvaluateExpression(functionCall.Arguments[i], varEnv, funcEnv, recEnv, store);
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

                EvaluateStatement(function.Statements, function.VarEnv, function.FuncEnv, recEnv, function.Store);

                return (null, varEnv, funcEnv, recEnv, store);

            case Assignment assignment:
                var assStore = EvaluateAssignment(assignment, varEnv, funcEnv, recEnv, store);
                return (null, varEnv, funcEnv, recEnv, assStore);

            case Increment increment:
                var incStore = EvaluateIncrement(increment, varEnv, funcEnv, recEnv, store);
                return (null, varEnv, funcEnv, recEnv, incStore);

            case Declaration declaration:
                var decEval = EvaluateDeclaration(declaration, varEnv, funcEnv, recEnv, store);
                var decStore = decEval.Item2;
                var decVarEnv = decEval.Item1;
                return (null, decVarEnv, funcEnv, recEnv, decStore);

            case Return returnStatement:
                var returnEval = EvaluateExpression(returnStatement.Expression, varEnv, funcEnv, recEnv, store);
                var returnVal = returnEval;
                var returnStore = returnEval;
                return (returnVal, varEnv, funcEnv, recEnv, store);
        }

        return (null, varEnv, funcEnv, recEnv, store);
    }

    public (VarEnv, Store) EvaluateDeclaration(Declaration declaration, VarEnv varEnv, FuncEnv funcEnv, RecEnv recEnv, Store store)
    {
        var val = EvaluateExpression(declaration.Expression, varEnv, funcEnv, recEnv, store);
        var declIdentifier = declaration.Identifier.Name;

        var prevIndex = varEnv.LookUp(declIdentifier);

        if (prevIndex != null)
        {
            store.Bind(prevIndex.Value, val);
            return (varEnv, store);
        }

        var next = varEnv.GetNext();
        varEnv.Bind(declIdentifier, next);
        store.Bind(next, val);
        return (varEnv, store);
    }

    public Store EvaluateAssignment(Assignment assignment, VarEnv varEnv, FuncEnv funcEnv, RecEnv recEnv, Store store)
    {

        if (assignment.Identifier.Attribute != null)
        {
            return EvaluateAttributeAssignment(assignment, varEnv, funcEnv, recEnv, store);
        }

        var expresEval = EvaluateExpression(assignment.Expression, varEnv, funcEnv, recEnv, store);
        var assignExpression = expresEval;
        var assignIdentifier = assignment.Identifier.Name;
        var assignIndex = varEnv.LookUp(assignIdentifier);

        if (assignIndex == null)
        {
            errors.Add($"Variable {assignIdentifier} not found");
            return store;
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

        return store;
    }

    public Store EvaluateAttributeAssignment(Assignment assignment, VarEnv varEnv, FuncEnv funcEnv, RecEnv recEnv, Store store)
    {
        var attribute = assignment.Identifier.Attribute;
        var identifier = assignment.Identifier.Name;
        var index = varEnv.LookUp(identifier);
        if (index == null)
        {
            errors.Add($"Variable {identifier} not found in the VariableTable");
            return store;
        }

        var variable = store.LookUp(index.Value);

        if (variable is not FinalType finalType)
        {
            errors.Add($"Variable {identifier} is not a FinalType");
            return store;
        }

        var assignExpression = EvaluateExpression(assignment.Expression, varEnv, funcEnv, recEnv, store);
        finalType.Fields.TryGetValue(attribute, out var assignVariable);

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

        finalType.Fields[attribute] = assignVariable;
        store.Bind(index.Value, UpdateFinalType(finalType, attribute, assignVariable));

        return store;
    }

    public Store EvaluateIncrement(Increment increment, VarEnv varEnv, FuncEnv funcEnv, RecEnv recEnv, Store store)
    {
        var incrementIdentifier = increment.Identifier.Name;
        var incrementVariableIndex = varEnv.LookUp(incrementIdentifier);
        if (incrementVariableIndex == null)
        {
            errors.Add($"Variable {incrementIdentifier} not found in the VariableTable");
            return store;
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

        return store;
    }

    public (VarEnv, FuncEnv) EvaluateFunctionDeclaration(FunctionDeclaration functionDeclaration, VarEnv varEnv, FuncEnv funcEnv, RecEnv recEnv, Store store)
    {
        var parameters = functionDeclaration.Parameters.Select(x => x.Identifier.Name).ToList();
        var statements = functionDeclaration.Statements;
        var functionDecl = new Function(parameters, statements, new VarEnv(varEnv), new FuncEnv(funcEnv), store);
        funcEnv.Bind(functionDeclaration.Identifier.Name, functionDecl);
        return (varEnv, funcEnv);
    }

    public (RecEnv, FuncEnv) EvaluateConstructorDeclaration(ConstructorDeclaration constructorDeclaration, VarEnv varEnv, FuncEnv funcEnv, RecEnv recEnv, Store store)
    {
        var record = constructorDeclaration.Type.Value;
        var parameters = constructorDeclaration.Parameters.Select(x => x.Identifier.Name).ToList();
        var statements = constructorDeclaration.Statements;
        var constructorDecl = new Function(parameters, statements, new VarEnv(varEnv), new FuncEnv(funcEnv), store, true);
        funcEnv.Parent.Bind(record, constructorDecl);
        return (recEnv, funcEnv);
    }

    public object? EvaluateExpression(Expression expression, VarEnv varEnv, FuncEnv funcEnv, RecEnv recEnv, Store store)
    {
        switch (expression)
        {
            case Num num:
                return EvaluateLiterals(num, varEnv, funcEnv, recEnv, store);
            case Boolean boolean:
                return EvaluateLiterals(boolean, varEnv, funcEnv, recEnv, store);
            case String stringTerm:
                return EvaluateLiterals(stringTerm, varEnv, funcEnv, recEnv, store);
            case Identifier identifier:
                return EvaluateLiterals(identifier, varEnv, funcEnv, recEnv, store);

            case Record record:
                return EvaluateRecords(record, varEnv, funcEnv, recEnv, store);

            case FunctionCallTerm functionCall:
                var function = funcEnv.LookUp(functionCall.Identifier.Name);
                if (function == null)
                {
                    errors.Add($"Function {functionCall.Identifier.Name} not found in the FunctionTable");
                    return null;
                }

                if (function.Parameters.Count != functionCall.Arguments.Count)
                {
                    errors.Add(
                        $"Function {functionCall.Identifier.Name} has {function.Parameters.Count} parameters, but {functionCall.Arguments.Count} arguments were provided");
                    return null;
                }

                for (var i = 0; i < function.Parameters.Count; i++)
                {
                    var parameter = function.Parameters[i];
                    var functionCallVal = EvaluateExpression(functionCall.Arguments[i], varEnv, funcEnv, recEnv, store);
                    var varIndex = function.VarEnv.LocalLookUp(parameter);
                    if (varIndex == null)
                    {
                        var next = varEnv.GetNext();
                        function.VarEnv.Bind(parameter, next);
                        function.Store.Bind(next, functionCallVal);
                    }
                    else
                    {
                        function.Store.Bind(varIndex.Value, functionCallVal);
                    }
                }


                var tuple = EvaluateStatement(function.Statements, function.VarEnv, function.FuncEnv, recEnv, function.Store);

                if(function.IsConstructor)
                    return varEnv.LookUp(functionCall.Identifier.Name);

                if (tuple.Item1 == null)
                    throw new Exception($"Function {functionCall.Identifier.Name} did not return a value");

                return tuple.Item1;

            case UnaryOp unaryOp:
                var val = EvaluateExpression(unaryOp.Expression, varEnv, funcEnv, recEnv, store);
                return unaryOp.Op switch
                {
                    "-" => -(float)val,
                    "!" => !(bool)val,
                    _ => throw new NotImplementedException()
                };

            case BinaryOp binaryOp:
                var left = EvaluateExpression(binaryOp.Left, varEnv, funcEnv, recEnv, store);
                var right = EvaluateExpression(binaryOp.Right, varEnv, funcEnv, recEnv, store);
                if ((binaryOp.Op == "/" || binaryOp.Op == "%") && (float)right == 0)
                    throw new Exception("Division by zero is not allowed.");

                return binaryOp.Op switch
                {
                    "+" => (float)left + (float)right,
                    "-" => (float)left - (float)right,
                    "*" => (float)left * (float)right,
                    "/" => (float)left / (float)right,
                    "%" => (float)left % (float)right,
                    "==" => (float)left == (float)right,
                    "!=" => (float)left != (float)right,
                    "<" => (float)left < (float)right,
                    ">" => (float)left > (float)right,
                    "<=" => (float)left <= (float)right,
                    ">=" => (float)left >= (float)right,
                    "&&" => (bool)left && (bool)right,
                    "||" => (bool)left || (bool)right,
                    _ => throw new NotImplementedException()
                };

            case Group group:
                var finalPoint = (FinalPoint)EvaluateExpression(group.Point, varEnv, funcEnv, recEnv, store);

                varEnv = varEnv.EnterScope();
                funcEnv = funcEnv.EnterScope();

                EvaluateStatement(group.Statements, varEnv, funcEnv, recEnv, store);
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

                if (listVariable is not FinalList destinedList1)
                {
                    errors.Add($"Variable {addToList.ListIdentifier.Name} is not a list");
                    return null;
                }

                var valueToAdd = EvaluateExpression(addToList.Value, varEnv, funcEnv, recEnv, store);
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

                var indexToRemove = Convert.ToInt32(EvaluateExpression(removeFromList.Index, varEnv, funcEnv, recEnv, store));

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

                var indexOfValue = Convert.ToInt32(EvaluateExpression(getFromList.Index, varEnv, funcEnv, recEnv, store));

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
                foreach (var expr in list.Expressions) values.Add(EvaluateExpression(expr, varEnv, funcEnv, recEnv, store));

                return new FinalList(values);
        }

        return null;
    }

    public object? EvaluateRecords(Record record, VarEnv varEnv, FuncEnv funcEnv, RecEnv recEnv, Store store)
    {
        var identifiers = record.Identifiers;
        var expressions = record.Expressions.Select(expr => EvaluateExpression(expr, varEnv, funcEnv, recEnv, store)).ToList();

        var dictionary = identifiers.Zip(expressions, (identifier, expression) => new { identifier, expression })
            .ToDictionary(x => x.identifier.Name, x => x.expression);

        switch (record.RecordType.Value)
        {
            case "Circle":
                dictionary.TryGetValue("center", out var centerObj);
                dictionary.TryGetValue("radius", out var radiusObj);
                dictionary.TryGetValue("stroke", out var strokeObj);
                dictionary.TryGetValue("color", out var colorObj);
                dictionary.TryGetValue("strokeColor", out var strokeColorObj);
                var center = centerObj != null ? (FinalPoint)centerObj : new FinalPoint(0, 0);
                var radius = radiusObj != null ? (float)radiusObj : 1.0f;
                var stroke = strokeObj != null ? (float)strokeObj : 1.0f;
                var color = colorObj != null ? (FinalColor)colorObj : new FinalColor(0, 0, 0, 1);
                var strokeColor = strokeColorObj != null ? (FinalColor)strokeColorObj : new FinalColor(0, 0, 0, 1);
                return new FinalCircle(center, radius, stroke, color, strokeColor) { Fields = dictionary };

            case "Rectangle":
                dictionary.TryGetValue("topLeft", out var topLeftObj);
                dictionary.TryGetValue("bottomRight", out var bottomRightObj);
                dictionary.TryGetValue("stroke", out strokeObj);
                dictionary.TryGetValue("color", out colorObj);
                dictionary.TryGetValue("strokeColor", out strokeColorObj);
                dictionary.TryGetValue("rounding", out var roundingObj);
                var topLeft = topLeftObj != null ? (FinalPoint)topLeftObj : new FinalPoint(0, 0);
                var bottomRight = bottomRightObj != null ? (FinalPoint)bottomRightObj : new FinalPoint(1, 1);
                stroke = strokeObj != null ? (float)strokeObj : 1.0f;
                color = colorObj != null ? (FinalColor)colorObj : new FinalColor(0, 0, 0, 1);
                strokeColor = strokeColorObj != null ? (FinalColor)strokeColorObj : new FinalColor(0, 0, 0, 1);
                var rounding = roundingObj != null ? (float)roundingObj : 0.0f;
                return new FinalRectangle(topLeft, bottomRight, stroke, color, strokeColor, rounding)
                    { Fields = dictionary };

            case "Point":
                dictionary.TryGetValue("x", out var xObj);
                dictionary.TryGetValue("y", out var yObj);
                var x = xObj != null ? (float)xObj : 0.0f;
                var y = yObj != null ? (float)yObj : 0.0f;
                return new FinalPoint(x, y) { Fields = dictionary };

            case "Color":
                dictionary.TryGetValue("red", out var redObj);
                dictionary.TryGetValue("green", out var greenObj);
                dictionary.TryGetValue("blue", out var blueObj);
                dictionary.TryGetValue("alpha", out var alphaObj);
                var red = redObj != null ? (float)redObj : 0.0f;
                var green = greenObj != null ? (float)greenObj : 0.0f;
                var blue = blueObj != null ? (float)blueObj : 0.0f;
                var alpha = alphaObj != null ? (float)alphaObj : 1.0f;
                return new FinalColor(red, green, blue, alpha) { Fields = dictionary };

            case "Ellipse":
                dictionary.TryGetValue("center", out var ellipseCenterObj);
                dictionary.TryGetValue("radiusX", out var ellipseRadiusXObj);
                dictionary.TryGetValue("radiusY", out var ellipseRadiusYObj);
                dictionary.TryGetValue("stroke", out var ellipseStrokeObj);
                dictionary.TryGetValue("color", out var ellipseColorObj);
                dictionary.TryGetValue("strokeColor", out var ellipseStrokeColorObj);
                var ellipseCenter = ellipseCenterObj != null ? (FinalPoint)ellipseCenterObj : new FinalPoint(0, 0);
                var ellipseRadiusX = ellipseRadiusXObj != null ? (float)ellipseRadiusXObj : 1.0f;
                var ellipseRadiusY = ellipseRadiusYObj != null ? (float)ellipseRadiusYObj : 1.0f;
                var ellipseStroke = ellipseStrokeObj != null ? (float)ellipseStrokeObj : 1.0f;
                var ellipseColor = ellipseColorObj != null ? (FinalColor)ellipseColorObj : new FinalColor(0, 0, 0, 1);
                var ellipseStrokeColor = ellipseStrokeColorObj != null
                    ? (FinalColor)ellipseStrokeColorObj
                    : new FinalColor(0, 0, 0, 1);
                return new FinalEllipse(ellipseCenter, ellipseRadiusX, ellipseRadiusY, ellipseStroke, ellipseColor,
                    ellipseStrokeColor) { Fields = dictionary };

            case "Triangle":
                dictionary.TryGetValue("point1", out var point1Obj);
                dictionary.TryGetValue("point2", out var point2Obj);
                dictionary.TryGetValue("point3", out var point3Obj);
                var point1 = point1Obj != null ? (FinalPoint)point1Obj : new FinalPoint(0, 0);
                var point2 = point2Obj != null ? (FinalPoint)point2Obj : new FinalPoint(0, 0);
                var point3 = point3Obj != null ? (FinalPoint)point3Obj : new FinalPoint(0, 0);
                var points = new List<FinalPoint> { point1, point2, point3 };
                dictionary.TryGetValue("stroke", out var triangleStrokeObj);
                dictionary.TryGetValue("color", out var triangleColorObj);
                dictionary.TryGetValue("strokeColor", out var triangleStrokeColorObj);
                var triangleStroke = triangleStrokeObj != null ? (float)triangleStrokeObj : 1.0f;
                var triangleColor =
                    triangleColorObj != null ? (FinalColor)triangleColorObj : new FinalColor(0, 0, 0, 1);
                var triangleStrokeColor = triangleStrokeColorObj != null
                    ? (FinalColor)triangleStrokeColorObj
                    : new FinalColor(0, 0, 0, 1);
                return new FinalTriangle(point1, points, triangleStroke, triangleColor, triangleStrokeColor)
                    { Fields = dictionary };

            case "Polygon":
                dictionary.TryGetValue("points", out var polygonPointsObj);
                dictionary.TryGetValue("stroke", out var polygonStrokeObj);
                dictionary.TryGetValue("color", out var polygonColorObj);
                dictionary.TryGetValue("strokeColor", out var polygonStrokeColorObj);
                var polygonPoints = polygonPointsObj != null ? (FinalList)polygonPointsObj : null;
                var polygonStroke = polygonStrokeObj != null ? (float)polygonStrokeObj : 1.0f;
                var polygonColor = polygonColorObj != null ? (FinalColor)polygonColorObj : new FinalColor(0, 0, 0, 1);
                var polygonStrokeColor = polygonStrokeColorObj != null
                    ? (FinalColor)polygonStrokeColorObj
                    : new FinalColor(0, 0, 0, 1);
                return new FinalPolygon(polygonPoints, polygonStroke, polygonColor, polygonStrokeColor)
                    { Fields = dictionary };

            case "Line":
                dictionary.TryGetValue("start", out var linePoint1Obj);
                dictionary.TryGetValue("end", out var linePoint2Obj);
                dictionary.TryGetValue("stroke", out var lineStrokeObj);
                dictionary.TryGetValue("color", out var lineColorObj);
                var linePoint1 = linePoint1Obj != null ? (FinalPoint)linePoint1Obj : new FinalPoint(0, 0);
                var linePoint2 = linePoint2Obj != null ? (FinalPoint)linePoint2Obj : new FinalPoint(1, 1);
                var lineStroke = lineStrokeObj != null ? (float)lineStrokeObj : 1.0f;
                var lineColor = lineColorObj != null ? (FinalColor)lineColorObj : new FinalColor(0, 0, 0, 1);
                return new FinalLine(linePoint1, linePoint2, lineStroke, lineColor) { Fields = dictionary };

            case "Arrow":
                dictionary.TryGetValue("start", out var arrowStartObj);
                dictionary.TryGetValue("end", out var arrowEndObj);
                dictionary.TryGetValue("stroke", out var arrowStrokeObj);
                dictionary.TryGetValue("color", out var arrowColorObj);
                var arrowStart = arrowStartObj != null ? (FinalPoint)arrowStartObj : new FinalPoint(0, 0);
                var arrowEnd = arrowEndObj != null ? (FinalPoint)arrowEndObj : new FinalPoint(1, 1);
                var arrowStroke = arrowStrokeObj != null ? (float)arrowStrokeObj : 1.0f;
                var arrowColor = arrowColorObj != null ? (FinalColor)arrowColorObj : new FinalColor(0, 0, 0, 1);
                return new FinalArrow(arrowStart, arrowEnd, arrowStroke, arrowColor) { Fields = dictionary };

            case "Text":
                dictionary.TryGetValue("point", out var textPositionObj);
                dictionary.TryGetValue("content", out var contentObj);
                dictionary.TryGetValue("color", out var textColorObj);
                dictionary.TryGetValue("font", out var fontObj);
                dictionary.TryGetValue("size", out var fontSizeObj);
                dictionary.TryGetValue("weight", out var fontWeightObj);
                var textPosition = textPositionObj != null ? (FinalPoint)textPositionObj : new FinalPoint(0, 0);
                var content = contentObj != null ? (string)contentObj : string.Empty;
                var textColor = textColorObj != null ? (FinalColor)textColorObj : new FinalColor(0, 0, 0, 1);
                var font = fontObj != null ? (string)fontObj : "Arial";
                var fontSize = fontSizeObj != null ? (float)fontSizeObj : 12.0f;
                var fontWeight = fontWeightObj != null ? (float)fontWeightObj : 400.0f;
                return new FinalText(content, textPosition, font, fontSize, fontWeight, textColor)
                    { Fields = dictionary };

            case "Square":
                dictionary.TryGetValue("topLeft", out var squareTopLeftObj);
                dictionary.TryGetValue("side", out var squareLengthObj);
                dictionary.TryGetValue("stroke", out var squareStrokeObj);
                dictionary.TryGetValue("color", out var squareColorObj);
                dictionary.TryGetValue("strokeColor", out var squareStrokeColorObj);
                dictionary.TryGetValue("rounding", out var squareRoundingObj);
                var squareTopLeft = squareTopLeftObj != null ? (FinalPoint)squareTopLeftObj : new FinalPoint(0, 0);
                var squareLength = squareLengthObj != null ? (float)squareLengthObj : 1.0f;
                var squareStroke = squareStrokeObj != null ? (float)squareStrokeObj : 1.0f;
                var squareColor = squareColorObj != null ? (FinalColor)squareColorObj : new FinalColor(0, 0, 0, 1);
                var squareStrokeColor = squareStrokeColorObj != null
                    ? (FinalColor)squareStrokeColorObj
                    : new FinalColor(0, 0, 0, 1);
                var squareRounding = squareRoundingObj != null ? (float)squareRoundingObj : 0.0f;
                return new FinalSquare(squareTopLeft, squareLength, squareStroke, squareColor, squareStrokeColor,
                    squareRounding) { Fields = dictionary };

            default:
                List<FinalType> finalTypes = new();
                foreach (var expr in record.Expressions)
                {
                    if (expr is Record record1)
                    {
                        finalTypes.Add((FinalType)EvaluateRecords(record1, varEnv, funcEnv, recEnv, store));
                    }
                }
                return new FinalRecord(finalTypes) { Fields = dictionary };
        }


        throw new Exception("FinalType: " + record.RecordType.Value + " not found");
    }


    public object? EvaluateLiterals(Expression expression, VarEnv varEnv, FuncEnv funcEnv, RecEnv recEnv, Store store)
    {
        switch (expression)
        {
            case Num num:
                return float.Parse(num.Value, CultureInfo.InvariantCulture);
            case Boolean boolean:
                return bool.Parse(boolean.Value);
            case String stringTerm:
                return stringTerm.Value.TrimStart('"').TrimEnd('"').Replace('\\', ' ');
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

                var finalType = variable as FinalType;

                if (finalType == null) return variable;

                if (finalType != null && identifier.Attribute != null) return finalType.Fields[identifier.Attribute];

                return finalType;
            default:
                throw new NotImplementedException();
        }
    }

    public object UpdateFinalType(FinalType finalType, string key, object value)
    {
        switch (finalType)
        {
            case FinalPoint finalPoint:
                switch (key)
                {
                    case "x":
                        finalPoint.X = new FinalNum((float)value);
                        break;
                    case "y":
                        finalPoint.Y = new FinalNum((float)value);
                        break;
                }

                return finalPoint;

            case FinalColor finalColor:
                switch (key)
                {
                    case "red":
                        finalColor.Red = new FinalNum((float)value);
                        break;
                    case "green":
                        finalColor.Green = new FinalNum((float)value);
                        break;
                    case "blue":
                        finalColor.Blue = new FinalNum((float)value);
                        break;
                    case "alpha":
                        finalColor.Alpha = new FinalNum((float)value);
                        break;
                }

                return finalColor;

            case FinalCircle finalCircle:
                switch (key)
                {
                    case "center":
                        finalCircle.Center = (FinalPoint)value;
                        break;
                    case "radius":
                        finalCircle.Radius = new FinalNum((float)value);
                        break;
                    case "stroke":
                        finalCircle.Stroke = new FinalNum((float)value);
                        break;
                    case "color":
                        finalCircle.FillColor = (FinalColor)value;
                        break;
                    case "strokeColor":
                        finalCircle.StrokeColor = (FinalColor)value;
                        break;

                }

                return finalCircle;

        }

        return finalType;

    }
}
