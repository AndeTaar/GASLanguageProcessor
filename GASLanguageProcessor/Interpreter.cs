using System.Globalization;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;
using Array = GASLanguageProcessor.AST.Expressions.Terms.Array;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using Expression = GASLanguageProcessor.AST.Expressions.Expression;
using Num = GASLanguageProcessor.AST.Expressions.Terms.Num;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;

namespace GASLanguageProcessor;

public class Interpreter
{
    public List<string> errors = new();

    public Store EvaluateProgram(AST.Expressions.Terms.Program program, VarEnv varEnv, FuncEnv funcEnv, Store store)
    {
        EvaluateStatement(program.Statements, varEnv, funcEnv, store);

        return store;
    }

    public (object?, VarEnv, FuncEnv, Store) EvaluateStatement(Statement statement, VarEnv varEnv, FuncEnv funcEnv,
        Store store)
    {
        switch (statement)
        {
            case Compound compound:
                var tuple = EvaluateStatement(compound.Statement1, varEnv, funcEnv, store);
                if (tuple.Item1 != null) return tuple;

                tuple = EvaluateStatement(compound.Statement2, varEnv, funcEnv, store);
                return tuple;

            // Currently allows infinite loops.
            case For @for:
                EvaluateStatement(@for.Initializer, varEnv, funcEnv, store);
                var val = EvaluateExpression(@for.Condition, varEnv, funcEnv, store);
                while ((bool)val)
                {
                    tuple = EvaluateStatement(@for.Statements, varEnv, funcEnv, store);
                    if (tuple.Item1 != null) return tuple;

                    EvaluateStatement(@for.Incrementer, varEnv, funcEnv, store);
                    val = EvaluateExpression(@for.Condition, varEnv, funcEnv, store);
                }

                return (null, varEnv, funcEnv, store);

            // Currently allows infinite loops.
            case While @while:
                val = EvaluateExpression(@while.Condition, varEnv, funcEnv, store);

                varEnv = varEnv.EnterScope();
                funcEnv = funcEnv.EnterScope();

                while ((bool)val)
                {
                    tuple = EvaluateStatement(@while.Statements, varEnv, funcEnv, store);
                    if (tuple.Item1 != null) return tuple;
                    val = EvaluateExpression(@while.Condition, varEnv.Parent, funcEnv.Parent, store);
                }

                return (null, varEnv, funcEnv, store);

            case If @if:
                val = EvaluateExpression(@if.Condition, varEnv, funcEnv, store);

                varEnv = varEnv.EnterScope();
                funcEnv = funcEnv.EnterScope();

                if ((bool)val) return EvaluateStatement(@if.Statements, varEnv, funcEnv, store);

                if (@if.Else != null) return EvaluateStatement(@if.Else, varEnv, funcEnv, store);

                return (null, varEnv, funcEnv, store);
            case FunctionDeclaration functionDeclaration:
                var funcDecl = EvaluateFunctionDeclaration(functionDeclaration, varEnv, funcEnv, store);
                return (null, funcDecl.Item1, funcDecl.Item2, store);

            case FunctionCallStatement functionCall:
                var function = funcEnv.LookUp(functionCall.Identifier.Name);
                if (function == null)
                {
                    errors.Add($"Function {functionCall.Identifier.Name} not found in the FunctionTable");
                    return (null, varEnv, funcEnv, store);
                }

                if (function.Parameters.Count != functionCall.Arguments.Count)
                {
                    errors.Add(
                        $"Function {functionCall.Identifier.Name} has {function.Parameters.Count} parameters, but {functionCall.Arguments.Count} arguments were provided");
                    return (null, varEnv, funcEnv, store);
                }
                var next = 0;
                for (var i = 0; i < function.Parameters.Count; i++)
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

                return (null, varEnv, funcEnv, store);

            case AddToArray addToList:
                var listVariableIndex = varEnv.LookUp(addToList.ListIdentifier.Name);

                if (listVariableIndex == null)
                {
                    errors.Add($"Variable {addToList.ListIdentifier.Name} not found in the VariableTable");
                    return (null, varEnv, funcEnv, store);
                }

                var listVariable = store.LookUp(listVariableIndex.Value);

                if (listVariable == null)
                {
                    errors.Add($"Variable {addToList.ListIdentifier.Name} not found in the Store");
                    return (null, varEnv, funcEnv, store);
                }

                if (listVariable is not FinalList destinedList1)
                {
                    errors.Add($"Variable {addToList.ListIdentifier.Name} is not a list");
                    return (null, varEnv, funcEnv, store);
                }

                var indexObj = EvaluateExpression(addToList.Index, varEnv, funcEnv, store);
                var index = Convert.ToInt32(indexObj);
                var valueToAdd = EvaluateExpression(addToList.Value, varEnv, funcEnv, store);
                if(index < 0 || index >= destinedList1.Values.Length)
                {
                    errors.Add($"Index {index} out of range for list {addToList.ListIdentifier.Name}");
                    return (null, varEnv, funcEnv, store);
                }

                destinedList1.Values[index] = valueToAdd;
                return (null, varEnv, funcEnv, store);

            case Assignment assignment:
                var assStore = EvaluateAssignment(assignment, varEnv, funcEnv, store);
                return (null, varEnv, funcEnv, assStore);

            case Increment increment:
                var incStore = EvaluateIncrement(increment, varEnv, funcEnv, store);
                return (null, varEnv, funcEnv, incStore);

            case Declaration declaration:
                var decEval = EvaluateDeclaration(declaration, varEnv, funcEnv, store);
                var decStore = decEval.Item2;
                var decVarEnv = decEval.Item1;
                return (null, decVarEnv, funcEnv, decStore);

            case Return returnStatement:
                var returnEval = EvaluateExpression(returnStatement.Expression, varEnv, funcEnv, store);
                var returnVal = returnEval;
                var returnStore = returnEval;
                return (returnVal, varEnv, funcEnv, store);
        }

        return (null, varEnv, funcEnv, store);
    }

    public (VarEnv, Store) EvaluateDeclaration(Declaration declaration, VarEnv varEnv, FuncEnv funcEnv, Store store)
    {
        var val = EvaluateExpression(declaration.Expression, varEnv, funcEnv, store);
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

    public Store EvaluateAssignment(Assignment assignment, VarEnv varEnv, FuncEnv funcEnv, Store store)
    {

        if (assignment.Identifier.Attribute != null)
        {
            return EvaluateAttributeAssignment(assignment, varEnv, funcEnv, store);
        }

        var expresEval = EvaluateExpression(assignment.Expression, varEnv, funcEnv, store);
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

    public Store EvaluateAttributeAssignment(Assignment assignment, VarEnv varEnv, FuncEnv funcEnv, Store store)
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

        var assignExpression = EvaluateExpression(assignment.Expression, varEnv, funcEnv, store);
        var assignVariable = finalType.Fields[attribute];

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

    public Store EvaluateIncrement(Increment increment, VarEnv varEnv, FuncEnv funcEnv, Store store)
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

    public (VarEnv, FuncEnv) EvaluateFunctionDeclaration(FunctionDeclaration functionDeclaration, VarEnv varEnv,
        FuncEnv funcEnv, Store store)
    {
        var parameters = functionDeclaration.Parameters.Select(x => x.Identifier.Name).ToList();
        var statements = functionDeclaration.Statements;
        var functionDecl = new Function(parameters, statements, new VarEnv(varEnv), new FuncEnv(funcEnv), store);
        funcEnv.Bind(functionDeclaration.Identifier.Name, functionDecl);
        return (varEnv, funcEnv);
    }

    public object? EvaluateExpression(Expression expression, VarEnv varEnv, FuncEnv funcEnv, Store store)
    {
        switch (expression)
        {
            case Num num:
                return EvaluateLiterals(num, varEnv, funcEnv, store);
            case Boolean boolean:
                return EvaluateLiterals(boolean, varEnv, funcEnv, store);
            case String stringTerm:
                return EvaluateLiterals(stringTerm, varEnv, funcEnv, store);
            case Identifier identifier:
                return EvaluateLiterals(identifier, varEnv, funcEnv, store);

            case Record record:
                return EvaluateRecords(record, varEnv, funcEnv, store);

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
                    var functionCallVal = EvaluateExpression(functionCall.Arguments[i], varEnv, funcEnv, store);
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

                var tuple = EvaluateStatement(function.Statements, function.VarEnv, function.FuncEnv, function.Store);

                if (tuple.Item1 == null)
                    throw new Exception($"Function {functionCall.Identifier.Name} did not return a value");

                return tuple.Item1;

            case UnaryOp unaryOp:
                var val = EvaluateExpression(unaryOp.Expression, varEnv, funcEnv, store);
                return unaryOp.Op switch
                {
                    "-" => -(float)val,
                    "!" => !(bool)val,
                    _ => throw new NotImplementedException()
                };

            case BinaryOp binaryOp:
                var left = EvaluateExpression(binaryOp.Left, varEnv, funcEnv, store);
                var right = EvaluateExpression(binaryOp.Right, varEnv, funcEnv, store);
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
                var finalPoint = (FinalPoint)EvaluateExpression(group.Point, varEnv, funcEnv, store);

                varEnv = varEnv.EnterScope();
                funcEnv = funcEnv.EnterScope();

                EvaluateStatement(group.Statements, varEnv, funcEnv, store);
                return new FinalGroup(finalPoint, varEnv);

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

                if (indexToRemove < 0 || indexToRemove >= destinedList.Values.Length)
                {
                    errors.Add($"Index {indexToRemove} out of range for list {removeFromList.ListIdentifier.Name}");
                    return null;
                }

                destinedList.Values[indexToRemove] = null;
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

                if (indexOfValue < 0 || indexOfValue >= sourceList.Values.Length)
                {
                    errors.Add($"Index {indexOfValue} out of range for list {getFromList.ListIdentifier.Name}");
                    return null;
                }

                var valueToGet = sourceList.Values[indexOfValue];

                return valueToGet;

            case SizeOfArray lengthOfList:
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

                return (float)listToCheckLength.Values.Length;

            case Array list:
                var size = (int) ((float) EvaluateExpression(list.Size, varEnv, funcEnv, store));
                object[] values = new object[size];
                for (var i = 0; i < list.Expressions.Count; i++)
                {
                    values[i] = EvaluateExpression(list.Expressions[i], varEnv, funcEnv, store);
                }

                return new FinalList(values);
        }

        return null;
    }

    public object? EvaluateRecords(Record record, VarEnv varEnv, FuncEnv funcEnv, Store store)
    {
        var identifiers = record.Identifiers;
        var expressions = record.Expressions.Select(expr => EvaluateExpression(expr, varEnv, funcEnv, store)).ToList();

        var dictionary = identifiers.Zip(expressions, (identifier, expression) => new { identifier, expression })
            .ToDictionary(x => x.identifier.Name, x => x.expression);
        return new FinalRecord(record.RecordType.Value) { Fields = dictionary, Id=record.connectedIdentifier };
    }

    public object? EvaluateLiterals(Expression expression, VarEnv varEnv, FuncEnv funcEnv, Store store)
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
