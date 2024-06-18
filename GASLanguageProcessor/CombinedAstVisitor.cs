using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Types;
using GASLanguageProcessor.AST.Types.RecordType;
using GASLanguageProcessor.AST.Types.StatementsType;
using GASLanguageProcessor.AST.Types.VariableType;
using GASLanguageProcessor.TableType;
using Array = GASLanguageProcessor.AST.Expressions.Terms.Array;
using Assignment = GASLanguageProcessor.AST.Statements.Assignment;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;
using ValueType = GASLanguageProcessor.AST.Types.ValueType;

namespace GASLanguageProcessor;

public class CombinedAstVisitor : IAstVisitor<GasType>
{
    public List<string> errors = new();

    /// <summary>
    ///     Visits the program node
    /// </summary>
    /// <param name="program"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitProgram(AST.Expressions.Terms.Program program, TypeEnv envT)
    {
        var returnType = program.Statements.Accept(this, envT);

        var statementType = returnType as StatementType;
        var errorType = returnType as ErrorType;
        var valueType = returnType as ValueType;

        if (statementType != null && statementType.Type != StatementTypes.Ok)
        {
            errors.Add("Invalid return type for program: expected: Ok, got: " + statementType);
            return new ErrorType();
        }

        if (errorType != null)
        {
            errors.Add("Invalid return type for program: expected: Ok, got: " + errorType);
            return new ErrorType();
        }

        if (valueType != null)
        {
            errors.Add("Invalid return type for program: expected: Ok, got: " + valueType);
            return new ErrorType();
        }

        return new StatementType(StatementTypes.Ok);
    }

    /// <summary>
    ///     Visits the compound node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitCompound(Compound node, TypeEnv envT)
    {
        var returnType = node.Statement1?.Accept(this, envT);
        var returnType2 = node.Statement2?.Accept(this, envT);
        if (returnType != null && !returnType.Equals(new StatementType(StatementTypes.Ok)))
            return returnType;
        return returnType2 ?? new StatementType(StatementTypes.Ok);
    }

    /// <summary>
    ///     Visits the if node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitIfStatement(If node, TypeEnv envT)
    {
        VariableType conditionType;
        try
        {
            conditionType = (VariableType)node.Condition.Accept(this, envT);
        }
        catch (Exception e)
        {
            errors.Add("Invalid type for condition: expected: Boolean, got: " + e.Message);
            return new ErrorType();
        }

        if (conditionType.Type != VariableTypes.Bool)
        {
            errors.Add("Invalid type for condition: expected: Boolean, got: " + conditionType);
            return new ErrorType();
        }

        envT = envT.EnterScope();
        var returnType = node.Statements?.Accept(this, envT);

        envT = envT.ExitScope();

        var @else = node.Else;

        if (@else is If @if)
        {
            returnType = @if.Accept(this, envT);
        }
        else if (@else != null)
        {
            envT = envT.EnterScope();
            returnType = @else?.Accept(this, envT);
        }

        return returnType ?? new StatementType(StatementTypes.Ok);
    }

    /// <summary>
    ///     Visits the function call statement node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitFunctionCallStatement(FunctionCallStatement node, TypeEnv envT)
    {
        var identifier = node.Identifier;
        var parametersAndReturn = envT.FLookUp(identifier.Name);

        if (parametersAndReturn == null)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name + " not found");
            return new ErrorType();
        }

        var expectedParameterTypes = parametersAndReturn.Value.Item1;
        var returnType = parametersAndReturn.Value.Item2;

        var parameterTypes = node.Arguments.Select(expression => expression.Accept(this, envT)).ToList();

        if (expectedParameterTypes.Count != parameterTypes.Count)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name +
                       " expecting arguments: \n" + expectedParameterTypes.Select(p => p.ToString())
                           .Aggregate((a, b) => a + ", " + b) +
                       "\n got arguments: \n" +
                       parameterTypes.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));
            return new ErrorType();
        }

        for (var i = 0; i < expectedParameterTypes.Count; i++)
            if (!parameterTypes[i].Equals(expectedParameterTypes[i]))
                errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name +
                           " expecting arguments: \n" + expectedParameterTypes.Select(p => p.ToString())
                               .Aggregate((a, b) => a + ", " + b) +
                           "\n got arguments: \n" +
                           parameterTypes.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));

        return new StatementType(StatementTypes.Ok);
    }

    /// <summary>
    ///     Visits the while node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitWhile(While node, TypeEnv envT)
    {
        envT = envT.EnterScope();
        var conditionType = (VariableType) node.Condition.Accept(this, envT);
        if (conditionType.Type != VariableTypes.Bool)
            errors.Add("Invalid type for condition: expected: Boolean, got: " + conditionType);
        var returnType = node.Statements?.Accept(this, envT);
        return returnType ?? new StatementType(StatementTypes.Ok);
    }

    /// <summary>
    ///     Visits the for node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitFor(For node, TypeEnv envT)
    {
        envT = envT.EnterScope();
        var initializer = (StatementType) node.Initializer.Accept(this, envT);

        if (initializer.Type != StatementTypes.Ok)
        {
            errors.Add("Invalid type for initializer: expected: Ok, got: " + initializer);
        }

        var incrementer = node.Incrementer.Accept(this, envT);
        var incrementerType = incrementer as StatementType;

        if (incrementerType?.Type != StatementTypes.Ok)
        {
            errors.Add("Invalid type for incrementer: expected: Ok, got: " + incrementer);
        }

        var condition = node.Condition.Accept(this, envT) as VariableType;

        if (condition?.Type != VariableTypes.Bool) errors.Add("Invalid type for condition: expected: Boolean, got: " + condition);

        var returnType = node.Statements?.Accept(this, envT);

        return returnType ?? new StatementType(StatementTypes.Ok);
    }

    /// <summary>
    ///     Visit the return node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitReturn(Return node, TypeEnv envT)
    {
        return node.Expression.Accept(this, envT);
    }

    /// <summary>
    ///     Visits the assignment node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitAssignment(Assignment node, TypeEnv envT)
    {
        var identifier = node.Identifier;

        if (node.Identifier.Attribute != null) return AttributeAssignment(node, envT);

        var variableType = envT.VLookUp(identifier.Name);

        if (node.Expression as Record != null) return RecordAssignment(node, envT);

        if (variableType == null)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: " + identifier.Name + " not found in scope");
            return new ErrorType();
        }

        var expressionType = node.Expression.Accept(this, envT) as VariableType;

        switch (node.Operator)
        {
            case "+=":
            case "-=":
            case "*=":
            case "/=":
                if (variableType != expressionType?.Type || variableType != VariableTypes.Num)
                    errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + variableType +
                               " got: " + expressionType);
                break;
            case "=":
                if (variableType != expressionType?.Type)
                    errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + variableType +
                               " got: " + expressionType);
                break;
            default:
                errors.Add("Invalid operator: " + node.Operator);
                break;
        }

        return new StatementType(StatementTypes.Ok);
    }

    /// <summary>
    ///     Visits the declaration node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitDeclaration(Declaration node, TypeEnv envT)
    {
        var type = node.Type.Accept(this, envT);
        if(type is ArrayType || node.Expression is Array)
            return ArrayDeclaration(node, envT);
        if(type is RecordType)
            return RecordDeclaration(node, envT);


        var variableType = (VariableType)type;
        var identifier = node.Identifier;
        var existingVariableType = envT.VLookUp(identifier.Name);

        if (existingVariableType != null)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: " + identifier.Name + " Can not redeclare variable");
            return new ErrorType();
        }

        var expression = node.Expression?.Accept(this, envT) as VariableType;

        if (expression != null && variableType?.Type != expression.Type)
        {
            errors.Add("Line: " + node.LineNum + " Invalid type for variable: " + identifier.Name + " expected: " +
                       type + " got: " + expression);
            return new ErrorType();
        }

        var bound = envT.VBind(identifier.Name, variableType.Type);

        if (!bound)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: " + identifier.Name + " already exists");
            return new ErrorType();
        }

        return new StatementType(StatementTypes.Ok);
    }

    private GasType ArrayDeclaration(Declaration node, TypeEnv envT)
    {
        if (!node.Type.Value.Contains("[]"))
        {
            node.Type.Value += "[]";
        }
        var type = (ArrayType) node.Type.Accept(this, envT);
        var identifier = node.Identifier;
        var existingVariableType = envT.ALookUp(identifier.Name);

        if (existingVariableType != null)
        {
            errors.Add("Line: " + node.LineNum + " Array name: " + identifier.Name + " Can not redeclare array");
            return new ErrorType();
        }

        var bound = envT.ABind(identifier.Name, type);

        if (!bound)
        {
            errors.Add("Line: " + node.LineNum + " Array name: " + identifier.Name + " already exists");
            return new ErrorType();
        }

        return new StatementType(StatementTypes.Ok);
    }

    /// <summary>
    ///     Visit the increment node
    /// </summary>
    /// <param name="increment"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitIncrement(Increment increment, TypeEnv envT)
    {
        var identifier = increment.Identifier;
        var op = increment.Operator;
        var variableType = envT.VLookUp(identifier.Name);

        if (variableType == null)
        {
            errors.Add("Line: " + increment.LineNum + " Variable name: " + identifier.Name + " not found in scope");
            return new ErrorType();
        }

        switch (op)
        {
            case "++":
            case "--":
                if (variableType != VariableTypes.Num)
                    errors.Add("Invalid type for variable: " + identifier.Name + " expected: Num, got: " +
                               variableType);

                break;
        }

        return new StatementType(StatementTypes.Ok);
    }

    public GasType VisitRecordDefinition(RecordDefinition node, TypeEnv envT)
    {
        var identifiers = node.Identifiers;
        var types = node.Types;
        var typeIdentDictionary = identifiers.Zip(types, (i, t) => new { i, t })
            .ToDictionary(x => x.i.Name, x => x.t.Accept(this, envT));

        envT.RecTypeBind(node.RecordType.Value, typeIdentDictionary, GasRecordTypes.AnyStruct);
        return new StatementType(StatementTypes.Ok);
    }

    /// <summary>
    ///     Visit the function declaration node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitFunctionDeclaration(FunctionDeclaration node, TypeEnv envT)
    {
        var identifier = node.Identifier.Name;
        envT = envT.EnterScope();

        var expectedParameterTypes = node.Parameters.Select(parameter =>
        {
            var type = parameter.Type.Accept(this, envT);
            var variableType = type as VariableType;
            var recordType = type as RecordType;
            var arrayType = type as ArrayType;
            if (recordType != null)
            {
                envT.RecBind(parameter.Identifier.Name, parameter.Type.Value, envT);
            }
            else if (variableType != null)
            {
                envT.VBind(parameter.Identifier.Name, variableType.Type);
            }
            else if (arrayType != null)
            {
                envT.ABind(parameter.Identifier.Name, arrayType);
            }
            else
            {
                errors.Add("Line: " + node.LineNum + " Invalid type for parameter: " + parameter.Identifier.Name);
            }

            return parameter.Type.Accept(this, envT);
        }).ToList();

        var expectedReturnType = node.ReturnType.Accept(this, envT);

        var returnType = node.Statements?.Accept(this, envT);

        if (!expectedReturnType.Equals(returnType) && !expectedReturnType.Equals(new StatementType(StatementTypes.Void)))
        {
            errors.Add("Line: " + node.LineNum + " Invalid return type for function: " + identifier + " expected: " +
                       expectedReturnType.ToString() + " got: " + returnType.ToString());
            return new ErrorType();
        }

        envT = envT.ExitScope();

        var bound = envT.FBind(identifier, expectedParameterTypes, expectedReturnType);
        if (bound == false)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier + " already exists");
            return new ErrorType();
        }

        return new StatementType(StatementTypes.Ok);
    }


    /// Expressions
    /// <summary>
    ///     Visit the num node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitNum(Num node, TypeEnv envT)
    {
        return new VariableType(VariableTypes.Num);
    }

    /// <summary>
    ///     Visit the boolean node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitBoolean(Boolean node, TypeEnv envT)
    {
        return new VariableType(VariableTypes.Bool);
    }

    /// <summary>
    ///     Visit the string node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitString(String node, TypeEnv envT)
    {
        return new VariableType(VariableTypes.String);
    }

    /// <summary>
    ///     Visit the identifier node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitIdentifier(Identifier node, TypeEnv envT)
    {
        var record = envT.RecLookUp(node.Name);
        var recordType = record?.Item1;
        var returnType = recordType?.Item2;
        var recordFieldTypes = recordType?.Item1;

        var array = envT.ALookUp(node.Name);
        var arrayType = array as ArrayType;

        if (node.Attribute != null)
        {
            if (record == null)
            {
                errors.Add("Line: " + node.LineNum + " Record name: " + node.Name + " not found");
                return new ErrorType();
            }

            var field = recordFieldTypes?[node.Attribute];

            if (field == null)
            {
                errors.Add("Line: " + node.LineNum + " Record name: " + node.Name + " does not contain field: " +
                           node.Attribute);
                return new ErrorType();
            }

            return field ?? new ErrorType();
        }

        if (record != null) return returnType != null ? new RecordType(returnType ?? GasRecordTypes.AnyStruct) : new ErrorType();

        if (array != null) return arrayType != null ? new ArrayType(arrayType.ElementType) : new ErrorType();

        var variableType = envT.VLookUp(node.Name);

        if (variableType == null)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: " + node.Name + " not found");
            return new ErrorType();
        }

        return new VariableType(variableType ?? VariableTypes.String);
    }

    public GasType VisitRecord(Record record, TypeEnv envT)
    {
        var expectedTypesAndReturnType = envT.RecTypeLookUp(record.RecordType.Value);
        var expectedTypes = expectedTypesAndReturnType?.Item1;
        var returnType = expectedTypesAndReturnType?.Item2;

        if (expectedTypes == null)
        {
            errors.Add("Line: " + record.LineNum + " Record type: " + record.RecordType.Value + " not found");
            return new ErrorType();
        }

        var identifiers = record.Identifiers;
        var expressions = record.Expressions.Select(expression => expression.Accept(this, envT)).ToList();

        var error = false;
        for (var i = 0; i < identifiers.Count; i++)
        {
            var contains = expectedTypes.TryGetValue(identifiers[i].Name, out var type);

            if (!contains)
            {
                errors.Add("Line: " + record.LineNum + " Record type: " + record.RecordType.Value +
                           " does not contain field: " + identifiers[i].Name);
                error = true;
                continue;
            }

            var expectedType = expectedTypes[identifiers[i].Name];
            var expectedVariableType = expectedType as VariableType;
            var expectedRecordType = expectedType as RecordType;
            var expectedArrayType = expectedType as ArrayType;

            var expressionType = expressions[i];
            var expressionVariableType = expressionType as VariableType;
            var expressionRecordType = expressionType as RecordType;
            var expressionArrayType = expressionType as ArrayType;

            if (expectedVariableType != null)
            {
                if (expectedVariableType.Type != expressionVariableType?.Type)
                {
                    errors.Add("Line: " + record.LineNum + " Invalid type for field: " + identifiers[i].Name +
                               " expected: " + expectedVariableType.Type + " got: " + expressionVariableType?.Type);
                    error = true;
                }
            }
            else if (expectedRecordType != null)
            {
                if (expectedRecordType.Type != expressionRecordType?.Type)
                {
                    errors.Add("Line: " + record.LineNum + " Invalid type for field: " + identifiers[i].Name +
                               " expected: " + expectedRecordType.Type + " got: " + expressionRecordType?.Type);
                    error = true;
                }
            }
            else if (expectedArrayType != null)
            {
                if (!expectedArrayType.ElementType.Equals(expressionArrayType?.ElementType))
                {
                    errors.Add("Line: " + record.LineNum + " Invalid type for field: " + identifiers[i].Name +
                               " expected: " + expectedArrayType.ToString() + " got: " + expressionType.ToString());
                    error = true;
                }
            }
            else
            {
                errors.Add("Line: " + record.LineNum + " Invalid type for field: " + identifiers[i].Name);
                error = true;
            }
        }

        if (error) return new ErrorType();

        return new RecordType(returnType ?? GasRecordTypes.AnyStruct);
    }

    /// <summary>
    ///     Visit the unary operation node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitUnaryOp(UnaryOp node, TypeEnv envT)
    {
        var expression = (VariableType) node.Expression.Accept(this, envT);
        var op = node.Op;

        switch (op)
        {
            case "!":
                if (expression?.Type == VariableTypes.Bool) return new VariableType(VariableTypes.Bool);
                errors.Add("Invalid type for unary operation: " + op + " expected: Boolean, got: " + expression);
                return new ErrorType();
            case "-":
                if (expression?.Type == VariableTypes.Num) return new VariableType(VariableTypes.Num);
                errors.Add("Invalid type for unary operation: " + op + " expected: Num, got: " + expression);
                return new ErrorType();
            default:
                errors.Add("Invalid operator: " + op);
                return new ErrorType();
        }
    }

    /// <summary>
    ///     Visit the binary operation node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitBinaryOp(BinaryOp node, TypeEnv envT)
    {
        var @operator = node.Op;
        VariableType left;
        VariableType right;
        try
        {
            left = (VariableType) node.Left.Accept(this, envT);
            right = (VariableType) node.Right.Accept(this, envT);
        }
        catch (Exception e)
        {
            errors.Add("Invalid types for binary operation: " + @operator + " expected: Num, got: " + e.Message);
            return new ErrorType();
        }


        switch (@operator)
        {
            case "+":
                if (left?.Type == VariableTypes.String && right?.Type == VariableTypes.String)
                {
                    return new VariableType(VariableTypes.String);
                }

                if (left?.Type == VariableTypes.Num && right?.Type == VariableTypes.Num)
                {
                    return new VariableType(VariableTypes.Num);
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: String or Num, got: " +
                           left?.ToString() + " and " + right?.ToString());
                return new ErrorType();

            case "-" or "*" or "/" or "%":
                if (left?.Type == VariableTypes.Num && right?.Type == VariableTypes.Num)
                {
                    return new VariableType(VariableTypes.Num);
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Num, got: " + left?.ToString() +
                           " and " + right?.ToString());
                return new ErrorType();

            case "<" or ">" or "<=" or ">=":
                if (left?.Type == VariableTypes.Num && right?.Type == VariableTypes.Num)
                {
                    return new VariableType(VariableTypes.Bool);
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Num, got: " + left?.ToString() +
                           " and " + right?.ToString());
                return new ErrorType();

            case "&&" or "||":
                if (left?.Type == VariableTypes.Bool && right?.Type == VariableTypes.Bool)
                {
                    return new VariableType(VariableTypes.Bool);
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Boolean, got: " + left +
                           " and " + right);
                return new ErrorType();

            case "==" or "!=":
                if ((left?.Type == VariableTypes.Bool && right?.Type == VariableTypes.Bool) ||
                    (left?.Type == VariableTypes.Num && right?.Type == VariableTypes.Num))
                {
                    return new VariableType(VariableTypes.Bool);
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Boolean or Number, got: " + left?.ToString() +
                           " and " + right?.ToString());
                return new ErrorType();


            default:
                errors.Add("Invalid operator: " + @operator);
                return new ErrorType();
        }
    }

    /// <summary>
    ///     Visit the type node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitType(Type node, TypeEnv envT)
    {
        bool array = node.Value.Contains("[]");
        node.Value = node.Value.Replace("[]", "");
        GasType type;
        switch (node.Value)
        {
            case "num":
                type = new VariableType(VariableTypes.Num);
                break;
            case "string":
                type = new VariableType(VariableTypes.String);
                break;
            case "text":
            case "Text":
                type = new RecordType(GasRecordTypes.Text);
                break;
            case "color":
            case "Color":
                type = new RecordType(GasRecordTypes.Color);
                break;
            case "boolean":
            case "bool":
                type = new VariableType(VariableTypes.Bool);
                break;
            case "Square":
            case "square":
                type = new RecordType(GasRecordTypes.Square);
                break;
            case "rectangle":
            case "Rectangle":
                type = new RecordType(GasRecordTypes.Rectangle);
                break;
            case "Point":
            case "point":
                type = new RecordType(GasRecordTypes.Point);
                break;
            case "line":
            case "Line":
                type = new RecordType(GasRecordTypes.Line);
                break;
            case "SegLine":
            case "segLine":
                type = new RecordType(GasRecordTypes.SegLine);
                break;
            case "Canvas":
            case "canvas":
                type = new RecordType(GasRecordTypes.Canvas);
                break;
            case "Circle":
            case "circle":
                type = new RecordType(GasRecordTypes.Circle);
                break;
            case "triangle":
            case "Triangle":
                type = new RecordType(GasRecordTypes.Triangle);
                break;
            case "linearGradient":
            case "LinearGradient":
                type = new RecordType(GasRecordTypes.Color);
                break;
            case "group":
                type = new GroupType();
                break;
            case "ellipse":
            case "Ellipse":
                type = new RecordType(GasRecordTypes.Ellipse);
                break;
            case "void":
                type = new StatementType(StatementTypes.Void);
                break;
            case "Polygon":
            case "polygon":
                type = new RecordType(GasRecordTypes.Polygon);
                break;
            case "arrow":
            case "Arrow":
                type = new RecordType(GasRecordTypes.Arrow);
                break;
            default:
                type = new RecordType(GasRecordTypes.AnyStruct);
                break;
        }

        if (array)
        {
            return new ArrayType(type);
        }

        return type;
    }

    /// <summary>
    ///     Visit the function call term node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitFunctionCallTerm(FunctionCallTerm node, TypeEnv envT)
    {
        var identifier = node.Identifier;
        var parametersAndReturnType = envT.FLookUp(identifier.Name);

        if (parametersAndReturnType == null)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name + " not found");
            return new ErrorType();
        }

        var expectedParameters = parametersAndReturnType?.Item1;
        var returnType = parametersAndReturnType?.Item2;

        var parameters = node.Arguments.Select(expression => expression.Accept(this, envT)).ToList();


        if (parameters.Count != expectedParameters?.Count)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name +
                       " expecting arguments: \n" +
                       expectedParameters?.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b) +
                       "\n got arguments: \n" + parameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));
            return new ErrorType();
        }

        for (var i = 0; i < expectedParameters.Count; i++)
        {
            if (!expectedParameters[i].Equals(parameters[i]))
            {
                errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name +
                           " expecting arguments: \n" +
                           expectedParameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b) +
                           "\n got arguments: \n" +
                           parameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));
            }
        }

        return returnType ?? new ErrorType();
    }

    /// <summary>
    ///     Visit the null node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitNull(Null node, TypeEnv envT)
    {
        return new NullType();
    }

    /// <summary>
    ///     Visit the group node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitGroup(Group node, TypeEnv envT)
    {
        ;
        var point = (RecordType) node.Point.Accept(this, envT);
        if (point.Type != GasRecordTypes.Point)
        {
            errors.Add("Invalid type for point: expected: Point, got: " + point);
            return new ErrorType();
        }

        envT = envT.EnterScope();
        node.Statements?.Accept(this, envT);
        return new GroupType();
    }

    /// <summary>
    ///     Visit the list node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitArray(Array node, TypeEnv envT)
    {
        var expressions = node.Expressions.Select(expression => expression.Accept(this, envT)).ToList();
        if (!node.Type.Value.Contains("[]"))
        {
            node.Type.Value += "[]";
        }

        var listType = (ArrayType) node.Type.Accept(this, envT);
        return expressions.All(l => l.Equals(listType.ElementType)) ? listType : new ErrorType();
    }

    public GasType VisitSkip(Skip node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitAddToArray(AddToArray addToArray, TypeEnv envT)
    {
        var listIdentifier = addToArray.ListIdentifier;
        var listType = envT.ALookUp(listIdentifier.Name);
        var expectedElementType = listType?.ElementType;
        var expectedVariableElementType = expectedElementType as VariableType;
        var expectedRecordElementType = expectedElementType as RecordType;
        var indexType = (VariableType) addToArray.Index.Accept(this, envT);
        var valueType = addToArray.Value.Accept(this, envT);
        var varValueType = valueType as VariableType;
        var recordValueType = valueType as RecordType;

        if (listType == null)
        {
            errors.Add("Line: " + addToArray.LineNum + " List name: " + listIdentifier.Name + " not found");
            return new ErrorType();
        }

        if (indexType.Type != VariableTypes.Num)
        {
            errors.Add("Line: " + addToArray.LineNum + " Invalid type for index: expected: Num, got: " + indexType.ToString());
            return new ErrorType();
        }

        if (expectedVariableElementType != null)
        {
            if (expectedVariableElementType.Type != varValueType?.Type)
            {
                errors.Add("Line: " + addToArray.LineNum + " Invalid type for value: expected: " +
                           expectedVariableElementType.Type +
                           " got: " + varValueType?.Type);
                return new ErrorType();
            }
        }

        if (expectedRecordElementType != null)
        {
            if (expectedRecordElementType.Type != recordValueType?.Type  && expectedRecordElementType.Type != GasRecordTypes.AnyStruct)
            {
                errors.Add("Line: " + addToArray.LineNum + " Invalid type for value: expected: " +
                           expectedRecordElementType.Type +
                           " got: " + recordValueType?.Type);
                return new ErrorType();
            }
        }

        return new StatementType(StatementTypes.Ok);
    }

    public GasType VisitGetFromArray(GetFromArray node, TypeEnv envT)
    {
        var listIdentifier = node.ListIdentifier;
        var listType = envT.ALookUp(listIdentifier.Name)?.ElementType;
        VariableType indexType;
        try
        {
            indexType = (VariableType) node.Index.Accept(this, envT);
        }
        catch (Exception e)
        {
            errors.Add("Line: " + node.LineNum + " Invalid type for index: expected: Num, got: " + e.Message);
            return new ErrorType();
        }

        if (listType == null)
        {
            errors.Add("Line: " + node.LineNum + " List name: " + listIdentifier.Name + " not found");
            return new ErrorType();
        }

        if (indexType?.Type != VariableTypes.Num)
        {
            errors.Add("Line: " + node.LineNum + " Invalid type for index: expected: Num, got: " + indexType.ToString());
            return new ErrorType();
        }

        return listType;
    }

    public GasType VisitLengthOfArray(SizeOfArray node, TypeEnv envT)
    {
        var listIdentifier = node.ListIdentifier;
        var listType = envT.VLookUp(listIdentifier.Name);

        if (listType == null)
        {
            errors.Add("Line: " + node.LineNum + " List name: " + listIdentifier.Name + " not found");
            return new ErrorType();
        }

        return new VariableType(VariableTypes.Num);
    }

    public GasType AttributeAssignment(Assignment node, TypeEnv envT){
        var identifier = node.Identifier;
        var record = envT.RecLookUp(identifier.Name);
        var recordType = record?.Item1;
        var recordFieldTypes = recordType?.Item1;
        var attributeType = recordFieldTypes?[identifier.Attribute];
        var varAttributeType = attributeType as VariableType;
        var recordAttributeType = attributeType as RecordType;
        var arrayAttributeType = attributeType as ArrayType;
        envT = record?.Item2;

        if (record == null)
        {
            errors.Add("Line: " + node.LineNum + " Record name: " + identifier.Name + " not found");
            return new ErrorType();
        }

        var expressionType = node.Expression?.Accept(this, envT);
        var varExpressionType = expressionType as VariableType;
        var recordExpressionType = expressionType as RecordType;
        var arrayExpressionType = expressionType as ArrayType;

        switch (node.Operator)
        {
            case "+=":
            case "-=":
            case "*=":
            case "/=":
                if (varAttributeType != null && varExpressionType != null)
                {
                    if (varAttributeType.Type != varExpressionType.Type || varAttributeType.Type != VariableTypes.Num)
                    {
                        errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + VariableTypes.Num +
                                   " got: " + varAttributeType.Type);
                    }
                }
                else
                {
                    errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + VariableTypes.Num +
                               " got: " + varAttributeType?.Type);
                }
                break;
            case "=":
                if (recordAttributeType != null && recordExpressionType != null)
                {
                    if (recordAttributeType.Type != recordExpressionType.Type)
                    {
                        errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + recordAttributeType.Type +
                                   " got: " + recordExpressionType.Type);
                    }
                }
                else if (arrayAttributeType != null && arrayExpressionType != null)
                {
                    if (arrayAttributeType.ElementType != arrayExpressionType.ElementType)
                    {
                        errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + arrayAttributeType.ElementType +
                                   " got: " + arrayExpressionType.ElementType);
                    }
                }
                else if(varAttributeType != null && varExpressionType != null)
                {
                    if (varAttributeType.Type != varExpressionType.Type)
                    {
                        errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + varAttributeType.Type +
                                   " got: " + varExpressionType.Type);
                    }
                }
                else
                {
                    errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + recordAttributeType?.Type +
                               " got: " + recordExpressionType?.Type);
                }
                break;
            default:
                errors.Add("Invalid operator: " + node.Operator);
                break;
        }

        return new StatementType(StatementTypes.Ok);
    }

    public GasType RecordAssignment(Assignment node, TypeEnv envT)
    {
        var identifier = node.Identifier;
        var recordType = envT.RecLookUp(identifier.Name);
        var expectedType = recordType?.Item1?.Item2;
        envT = envT.EnterScope();
        envT.TypeEnvParent.RecBind(identifier.Name, recordType?.Item3, envT);

        if (expectedType == null)
        {
            errors.Add("Line: " + node.LineNum + " Record name: " + identifier.Name + " not declared");
            return new ErrorType();
        }

        RecordType expressionType;
        try
        {
            expressionType = (RecordType) node.Expression.Accept(this, envT);
        }
        catch (Exception e)
        {
            errors.Add("Line: " + node.LineNum + " Invalid type for variable: " + identifier.Name + " expected: " +
                       expectedType + " got: " + e.Message);
            return new ErrorType();
        }

        if (expectedType != expressionType.Type)
        {
            errors.Add("Line: " + node.LineNum + " Invalid type for variable: " + identifier.Name + " expected: " +
                       expectedType + " got: " + expressionType?.Type);
            return new ErrorType();
        }

        return new StatementType(StatementTypes.Ok);
    }

    /// <summary>
    ///     Record declaration
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType RecordDeclaration(Declaration node, TypeEnv envT)
    {
        var identifier = node.Identifier;
        var variableType = envT.RecLookUp(identifier.Name);
        var expectedType = (RecordType) node.Type.Accept(this, envT);

        if (variableType != null)
        {
            errors.Add("Line: " + node.LineNum + " Record name: " + identifier.Name + " Can not redeclare record");
            return new ErrorType();
        }

        envT = envT.EnterScope();

        var expressionType = node.Expression?.Accept(this, envT) as RecordType;

        if (expressionType != null && expectedType.Type != expressionType?.Type)
        {
            errors.Add("Line: " + node.LineNum + " Invalid type for variable: " + identifier.Name + " expected: " +
                       expectedType.Type + " got: " + expressionType?.Type);
            return new ErrorType();
        }

        var bound = envT.TypeEnvParent?.RecBind(identifier.Name, node.Type.Value, envT);

        if (!bound ?? false)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: " + identifier.Name + " already exists");
            return new ErrorType();
        }

        return new StatementType(StatementTypes.Ok);
    }

}
