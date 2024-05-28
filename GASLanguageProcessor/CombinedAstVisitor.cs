using GASLanguageProcessor.AST;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using Expression = System.Linq.Expressions.Expression;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor;

public class CombinedAstVisitor: IAstVisitor<GasType>
{
    public List<string> errors = new();

    /// <summary>
    /// Visits the program node
    /// </summary>
    /// <param name="program"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitProgram(AST.Expressions.Terms.Program program, TypeEnv envT)
    {
        var returnType = program.Statements.Accept(this, envT);
        if(returnType != GasType.Ok)
        {
            errors.Add("Invalid return type for program: expected: Ok, got: " + returnType);
            return GasType.Error;
        }

        if (envT.VLookUp("canvas") == null)
        {
            errors.Add("Program missing canvas");
            return GasType.Error;
        }

        return GasType.Ok;
    }

    /// <summary>
    /// Visits the canvas node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitCanvas(Canvas node, TypeEnv envT)
    {
        var widthType = node.Width.Accept(this, envT);

        if(widthType != GasType.Num)
        {
            errors.Add("Invalid type for canvas width: expected: Num, got: " + widthType);
        }

        var heightType = node.Height.Accept(this, envT);

        if(heightType != GasType.Num)
        {
            errors.Add("Invalid type for canvas height: expected: Num, got: " + heightType);
        }

        var backgroundColorType = node.BackgroundColor?.Accept(this, envT);

        if(backgroundColorType != GasType.Color)
        {
            errors.Add("Invalid type for canvas background color: expected: Color, got: " + backgroundColorType);
        }
        var bound = envT.VBind("canvas", GasType.Canvas);

        if (!bound)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: canvas cannot be redeclared");
            return GasType.Error;
        }

        return GasType.Ok;
    }

    /// <summary>
    /// Visits the compound node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitCompound(Compound node, TypeEnv envT)
    {
        var returnType = node.Statement1?.Accept(this, envT);
        var returnType2 = node.Statement2?.Accept(this, envT);
        return (returnType != GasType.Ok ? returnType : returnType2) ?? GasType.Ok;
    }

    /// <summary>
    /// Visits the if node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitIfStatement(If node, TypeEnv envT)
    {
        var conditionType = node.Condition.Accept(this, envT);

        if(conditionType != GasType.Bool)
        {
            errors.Add("Invalid type for condition: expected: Boolean, got: " + conditionType);
            return GasType.Error;
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

        return returnType ?? GasType.Ok;
    }

    /// <summary>
    /// Visits the function call statement node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitFunctionCallStatement(FunctionCallStatement node, TypeEnv envT)
    {
        var identifier = node.Identifier;
        var parametersAndReturn = envT.FLookUp(identifier.Name);

        if(parametersAndReturn == null)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name + " not found");
            return GasType.Error;
        }

        var expectedParameterTypes = parametersAndReturn.Value.Item1;
        var returnType = parametersAndReturn.Value.Item2;

        var parameterTypes = node.Arguments.Select(expression => expression.Accept(this, envT)).ToList();

        if (expectedParameterTypes.Count != parameterTypes.Count)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name +
                       " expecting arguments: \n" + expectedParameterTypes.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b) +
                       "\n got arguments: \n" + parameterTypes.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));
            return GasType.Error;
        }

        for (int i = 0; i < expectedParameterTypes.Count; i++)
        {
            if (parameterTypes[i] != expectedParameterTypes[i] && parameterTypes[i] != GasType.Any && expectedParameterTypes[i] != GasType.Any)
            {
                errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name +
                           " expecting arguments: \n" + expectedParameterTypes.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b) +
                           "\n got arguments: \n" + parameterTypes.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));
            }
        }

        return GasType.Ok;
    }

    /// <summary>
    /// Visits the while node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitWhile(While node, TypeEnv envT)
    {
        envT = envT.EnterScope();
        var conditionType = node.Condition.Accept(this, envT);
        if(conditionType != GasType.Bool)
        {
            errors.Add("Invalid type for condition: expected: Boolean, got: " + conditionType);
        }
        var returnType = node.Statements?.Accept(this, envT);
        return returnType ?? GasType.Ok;
    }

    /// <summary>
    /// Visits the for node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitFor(For node, TypeEnv envT)
    {
        envT = envT.EnterScope();
        var initializer = node.Initializer?.Accept(this, envT);

        if (initializer != GasType.Ok && initializer != GasType.Error)
        {
            errors.Add("Invalid type for initializer: expected: Ok, got: " + initializer);
        }

        var incrementer = node.Incrementer.Accept(this, envT);

        if (incrementer != GasType.Ok && incrementer != GasType.Error)
        {
            errors.Add("Invalid type for incrementer: expected: Ok, got: " + incrementer);
        }

        var condition = node.Condition.Accept(this, envT);

        if(condition != GasType.Bool)
        {
            errors.Add("Invalid type for condition: expected: Boolean, got: " + condition);
        }

        var returnType = node.Statements?.Accept(this, envT);

        return returnType ?? GasType.Ok;
    }

    /// <summary>
    /// Visit the return node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitReturn(Return node, TypeEnv envT)
    {
        return node.Expression.Accept(this, envT);
    }

    /// <summary>
    /// Visits the assignment node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
     public GasType VisitAssignment(Assignment node, TypeEnv envT)
    {
        var identifier = node.Identifier;
        var variableType = envT.VLookUp(identifier.Name);

        if (variableType == null)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: " + identifier + " not found in scope");
            return GasType.Error;
        }

        var expressionType = node.Expression.Accept(this, envT);

        switch (node.Operator)
        {
            case "+=":
            case "-=":
            case "*=":
            case "/=":
                if (variableType != expressionType || variableType != GasType.Num)
                {
                    errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + variableType + " got: " + expressionType);
                }
                break;
            case "=":
                if (variableType != expressionType)
                {
                    errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + variableType + " got: " + expressionType);
                }
                break;
            default:
                errors.Add("Invalid operator: " + node.Operator);
                break;
        }

        return GasType.Ok;
    }

    /// <summary>
    /// Visits the declaration node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitDeclaration(Declaration node, TypeEnv envT)
    {
        var identifier = node.Identifier;
        var variableType = envT.VLookUp(identifier.Name);
        var type = node.Type.Accept(this, envT);

        if(variableType != null)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: " + identifier.Name + " Can not redeclare variable");
            return GasType.Error;
        }

        var expression = node.Expression?.Accept(this, envT);

        if (expression != null && expression != GasType.Any && type != expression)
        {
            errors.Add("Line: " + node.LineNum + " Invalid type for variable: " + identifier.Name + " expected: " + type + " got: " + expression);
            return GasType.Error;
        }

        var bound = envT.VBind(identifier.Name, type);

        if (!bound)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: " + identifier.Name + " already exists");
            return GasType.Error;
        }

        return GasType.Ok;
    }

    /// <summary>
    /// Visit the increment node
    /// </summary>
    /// <param name="increment"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitIncrement(Increment increment, TypeEnv envT)
    {
        var identifier = increment.Identifier;
        var op = increment.Operator;
        var variableType = envT.VLookUp(identifier.Name);

        if(variableType == null)
        {
            errors.Add("Line: " + increment.LineNum + " Variable name: " + identifier.Name + " not found in scope");
            return GasType.Error;
        }

        switch (op)
        {
            case "++":
            case "--":
                if (variableType != GasType.Num)
                {
                    errors.Add("Invalid type for variable: " + identifier.Name + " expected: Num, got: " + variableType);
                }

                break;
        }

        return GasType.Ok;
    }

    /// <summary>
    /// Visit the function declaration node
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
            envT.VBind(parameter.Identifier.Name, type);
            return parameter.Type.Accept(this, envT);
        }).ToList();

        var expectedReturnType = node.ReturnType.Accept(this, envT);

        var returnType = node.Statements?.Accept(this, envT);

        if(expectedReturnType != returnType && expectedReturnType != GasType.Void)
        {
            errors.Add("Line: " + node.LineNum + " Invalid return type for function: " + identifier + " expected: " + expectedReturnType + " got: " + returnType);
            return GasType.Error;
        }

        envT = envT.ExitScope();

        var bound = envT.FBind(identifier, expectedParameterTypes, expectedReturnType);
        if (bound == false)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier + " already exists");
            return GasType.Error;
        }

        return GasType.Ok;
    }


    /**
     * Expressions
     */


    /// <summary>
    /// Visit the num node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitNum(Num node, TypeEnv envT)
    {
        return GasType.Num;
    }

    /// <summary>
    /// Visit the boolean node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitBoolean(Boolean node, TypeEnv envT)
    {
        return GasType.Bool;
    }

    /// <summary>
    /// Visit the string node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitString(String node, TypeEnv envT)
    {
        return GasType.String;
    }

    /// <summary>
    /// Visit the identifier node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitIdentifier(Identifier node, TypeEnv envT)
    {
        var variableType = envT.VLookUp(node.Name);
        if(variableType == null){
            errors.Add("Line: " + node.LineNum + " Variable name: " + node.Name + " not found");
            return GasType.Error;
        }
        return variableType ?? GasType.Error;
    }

    /// <summary>
    /// Visit the unary operation node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitUnaryOp(UnaryOp node, TypeEnv envT)
    {
        var expression = node.Expression?.Accept(this, envT);
        var op = node.Op;

        switch (op)
        {
            case "!":
                if (expression == GasType.Bool)
                {
                    return GasType.Bool;
                }
                errors.Add("Invalid type for unary operation: " + op + " expected: Boolean, got: " + expression);
                return GasType.Error;
            case "-":
                if (expression == GasType.Num)
                {
                    return GasType.Num;
                }
                errors.Add("Invalid type for unary operation: " + op + " expected: Num, got: " + expression);
                return GasType.Error;
            default:
                errors.Add("Invalid operator: " + op);
                return GasType.Error;
        }
    }

    /// <summary>
    /// Visit the binary operation node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitBinaryOp(BinaryOp node, TypeEnv envT)
    {
        var @operator = node.Op;
        var left = node.Left.Accept(this, envT);
        var right = node.Right.Accept(this, envT);

        switch (@operator)
        {
            case "+":
                if (left == GasType.String && right == GasType.String)
                {
                    node.Type = GasType.String;
                    return GasType.String;
                }

                if (left == GasType.Num && right == GasType.Num)
                {
                    node.Type = GasType.Num;
                    return GasType.Num;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: String or Num, got: " +
                           left + " and " + right);
                return GasType.Error;

            case "-" or "*" or "/" or "%":
                if (left == GasType.Num && right == GasType.Num)
                {
                    node.Type = GasType.Num;
                    return GasType.Num;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Num, got: " + left +
                           " and " + right);
                return GasType.Error;

            case "<" or ">" or "<=" or ">=":
                if (left == GasType.Num && right == GasType.Num)
                {
                    node.Type = GasType.Bool;
                    return GasType.Bool;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Num, got: " + left +
                           " and " + right);
                return GasType.Error;

            case "&&" or "||":
                if (left == GasType.Bool && right == GasType.Bool)
                {
                    node.Type = GasType.Bool;
                    return GasType.Bool;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Boolean, got: " + left +
                           " and " + right);
                return GasType.Error;

            case "==" or "!=":
                if ((left == GasType.Bool && right == GasType.Bool) ||
                    (left == GasType.Num && right == GasType.Num))
                {
                    node.Type = GasType.Bool;
                    return GasType.Bool;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Boolean, got: " + left +
                           " and " + right);
                return GasType.Error;


            default:
                errors.Add("Invalid operator: " + @operator);
                return GasType.Error;
        }
    }

    /// <summary>
    /// Visit the type node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitType(Type node, TypeEnv envT)
    {
        node.Value = node.Value.Replace("list<", "").Replace(">", "");
        switch (node.Value)
        {
            case "num":
                return GasType.Num;
            case "string":
                return GasType.String;
            case "text":
                return GasType.Text;
            case "color":
                return GasType.Color;
            case "boolean":
                return GasType.Bool;
            case "square":
                return GasType.Square;
            case "rectangle":
                return GasType.Rectangle;
            case "point":
                return GasType.Point;
            case "line":
                return GasType.Line;
            case "segLine":
                return GasType.SegLine;
            case "circle":
                return GasType.Circle;
            case "bool":
                return GasType.Bool;
            case "group":
                return GasType.Group;
            case "ellipse":
                return GasType.Ellipse;
            case "void":
                return GasType.Void;
            case "polygon":
                return GasType.Polygon;
            case "arrow":
                return GasType.Arrow;
        }
        errors.Add(node.Value + " Not implemented");
        return GasType.Error;
    }

    /// <summary>
    /// Visit the function call term node
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
            return GasType.Error;
        }

        var expectedParameters = parametersAndReturnType?.Item1;
        var returnType = parametersAndReturnType?.Item2;

        var parameters = node.Arguments.Select(expression => expression.Accept(this, envT)).ToList();



        if (parameters.Count != expectedParameters?.Count)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name +
                       " expecting arguments: \n" + expectedParameters?.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b) +
                       "\n got arguments: \n" + parameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));
            return GasType.Error;
        }

        for (int i = 0; i < expectedParameters.Count; i++)
        {
            if (expectedParameters[i] != GasType.Any && parameters[i] != GasType.Any && expectedParameters[i] != parameters[i])
            {
                errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name +
                           " expecting arguments: \n" + expectedParameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b) +
                           "\n got arguments: \n" + parameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));
            }
        }

        return returnType ?? GasType.Error;
    }

    /// <summary>
    /// Visit the null node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitNull(Null node, TypeEnv envT)
    {
        return GasType.Null;
    }

    /// <summary>
    /// Visit the group node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitGroup(Group node, TypeEnv envT)
    { ;
        var point = node.Point.Accept(this, envT);
        if (point != GasType.Point)
        {
            errors.Add("Invalid type for point: expected: Point, got: " + point);
            return GasType.Error;
        }
        envT = envT.EnterScope();
        node.Statements?.Accept(this, envT);
        return GasType.Group;
    }

    /// <summary>
    /// Visit the list node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="envT"></param>
    /// <returns></returns>
    public GasType VisitList(List node, TypeEnv envT)
    {
        var list = node.Expressions.Select(expression => expression.Accept(this, envT)).ToList();
        var listType = node.Type.Accept(this,envT);
        return list.All(l => l == listType) ? listType : GasType.Error;
    }

    public GasType VisitSkip(Skip node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitAddToList(AddToList addToList, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitLine(SegLine node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitText(Text node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitCircle(Circle node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitRectangle(Rectangle node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitPoint(Point node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitColor(Color node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitSquare(Square node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitEllipse(Ellipse node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitSegLine(SegLine node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitLine(Line node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitGetFromList(GetFromList node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitRemoveFromList(RemoveFromList node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitArrow(Arrow node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitLengthOfList(LengthOfList node, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitPolygon(Polygon polygon, TypeEnv envT)
    {
        throw new NotImplementedException();
    }

    public GasType VisitTriangle(Triangle triangle, TypeEnv envT)
    {
        throw new NotImplementedException();
    }
}
