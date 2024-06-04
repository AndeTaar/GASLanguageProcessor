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
    public float canvasWidth;
    public float canvasHeight;
    public List<string> errors = new();

    public Store EvaluateProgram(AST.Expressions.Terms.Program program, VarEnv varEnv, FuncEnv funcEnv, Store store)
    {
        EvaluateStatement(program.Statements, varEnv, funcEnv, store);

        return store;
    }

    public (object?, VarEnv, FuncEnv, Store) EvaluateStatement(Statement statement, VarEnv varEnv, FuncEnv funcEnv, Store store) {
        switch (statement)
        {
            case Canvas canvas:
                var duple = EvaluateExpression(canvas.Width, varEnv, funcEnv, store);
                canvasWidth = (float) duple;
                duple = EvaluateExpression(canvas.Height, varEnv, funcEnv, store);
                canvasHeight = (float) duple;
                duple = EvaluateExpression(canvas.BackgroundColor, varEnv, funcEnv, store);
                var backgroundColor = (FinalColor) duple;
                var finalCanvas = new FinalCanvas(canvasWidth, canvasHeight, backgroundColor);

                int next = varEnv.GetNext();
                varEnv.Bind("canvas",  next);
                store.Bind(next, finalCanvas);
                return (null, varEnv, funcEnv, store);

            case Compound compound:
                var tuple = EvaluateStatement(compound.Statement1, varEnv, funcEnv, store);
                if (tuple.Item1 != null)
                {
                    return tuple;
                }

                tuple = EvaluateStatement(compound.Statement2, varEnv, funcEnv, store);
                return tuple;

            // Currently allows infinite loops.
            case For @for:
                EvaluateStatement(@for.Initializer, varEnv, funcEnv, store);
                duple = EvaluateExpression(@for.Condition, varEnv, funcEnv, store);
                while ((bool) duple)
                {
                    tuple = EvaluateStatement(@for.Statements, varEnv, funcEnv, store);
                    if (tuple.Item1 != null)
                    {
                        return tuple;
                    }
                    EvaluateStatement(@for.Incrementer, varEnv, funcEnv, store);
                    duple = EvaluateExpression(@for.Condition, varEnv, funcEnv, store);
                }

                return (null, varEnv, funcEnv, store);

            // Currently allows infinite loops.
            case While @while:
                duple = EvaluateExpression(@while.Condition, varEnv, funcEnv, store);

                varEnv = varEnv.EnterScope();
                funcEnv = funcEnv.EnterScope();

                while ((bool) duple)
                {
                    tuple = EvaluateStatement(@while.Statements, varEnv, funcEnv, store);
                    if (tuple.Item1 != null) return tuple;
                    duple = EvaluateExpression(@while.Condition, varEnv.Parent, funcEnv.Parent, store);
                }
                return (null, varEnv, funcEnv, store);

            case If @if:
                duple = EvaluateExpression(@if.Condition, varEnv, funcEnv, store);

                varEnv = varEnv.EnterScope();
                funcEnv = funcEnv.EnterScope();

                if ((bool)duple)
                {
                    return EvaluateStatement(@if.Statements, varEnv, funcEnv, store);
                }

                if (@if.Else != null)
                {
                    return EvaluateStatement(@if.Else, varEnv, funcEnv, store);
                }

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
                    errors.Add($"Function {functionCall.Identifier.Name} has {function.Parameters.Count} parameters, but {functionCall.Arguments.Count} arguments were provided");
                    return (null, varEnv, funcEnv, store);
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
        int next = varEnv.GetNext();
        varEnv.Bind(declIdentifier, next);
        store.Bind(next, val);
        return (varEnv, store);
    }

    public Store EvaluateAssignment(Assignment assignment, VarEnv varEnv, FuncEnv funcEnv, Store store)
    {
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

    public (VarEnv, FuncEnv) EvaluateFunctionDeclaration(FunctionDeclaration functionDeclaration, VarEnv varEnv, FuncEnv funcEnv, Store store)
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

                var tuple = EvaluateStatement(function.Statements, function.VarEnv, function.FuncEnv, function.Store);

                if (tuple.Item1 == null)
                {
                    throw new Exception($"Function {functionCall.Identifier.Name} did not return a value");
                }

                return (tuple, store);

            case UnaryOp unaryOp:
                var duple = EvaluateExpression(unaryOp.Expression, varEnv, funcEnv, store);
                return unaryOp.Op switch
                {
                    "-" => (-(float) duple, duple),
                    "!" => (!(bool) duple, duple),
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
                    "+" => ((float)left + (float)right, left),
                    "-" => ((float)left - (float)right, left),
                    "*" => ((float)left * (float)right, left),
                    "/" => ((float)left / (float)right, left),
                    "%" => ((float)left % (float)right, left),
                    "==" => ((float)left == (float)right, left),
                    "!=" => ((float)left != (float)right, left),
                    "<" => ((float)left < (float)right, left),
                    ">" => ((float)left > (float)right, left),
                    "<=" => ((float)left <= (float)right, left),
                    ">=" => ((float)left >= (float)right, left),
                    "&&" => ((bool)left && (bool)right, left),
                    "||" => ((bool)left || (bool)right, left),
                    _ => throw new NotImplementedException()
                };

            case Group group:
                var finalPoint = (FinalPoint) EvaluateExpression(group.Point, varEnv, funcEnv, store);

                varEnv = varEnv.EnterScope();
                funcEnv = funcEnv.EnterScope();

                EvaluateStatement(group.Statements, varEnv, funcEnv, store);
                return (new FinalGroup(finalPoint, varEnv), store);

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

    public object? EvaluateLiterals(Expression expression, VarEnv varEnv, FuncEnv funcEnv, Store store)
    {
        switch (expression)
        {
            case Num num:
                return (float.Parse(num.Value, CultureInfo.InvariantCulture), store);
            case Boolean boolean:
                return (bool.Parse(boolean.Value), store);
            case String stringTerm:
                return (stringTerm.Value.TrimStart('"').TrimEnd('"').Replace('\\', ' '), store);
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

                return (variable, store);
            default:
                throw new NotImplementedException();
        }
    }
}
