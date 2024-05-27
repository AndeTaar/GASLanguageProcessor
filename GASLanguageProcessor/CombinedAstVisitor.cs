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
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitProgram(AST.Expressions.Terms.Program program, Scope scope)
    {
        program.Scope = scope;
        var returnType = program.Statements.Accept(this, scope);
        if(returnType != GasType.Ok)
        {
            errors.Add("Invalid return type for program: expected: Ok, got: " + returnType);
            return GasType.Error;
        }

        if (scope.vTable.LocalLookUp("canvas") == null)
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
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitCanvas(Canvas node, Scope scope)
    {
        node.Scope = scope;
        var widthType = node.Width.Accept(this, scope);

        if(widthType != GasType.Num)
        {
            errors.Add("Invalid type for canvas width: expected: Num, got: " + widthType);
        }

        var heightType = node.Height.Accept(this, scope);

        if(heightType != GasType.Num)
        {
            errors.Add("Invalid type for canvas height: expected: Num, got: " + heightType);
        }

        var backgroundColorType = node.BackgroundColor?.Accept(this, scope);

        if(backgroundColorType != GasType.Color)
        {
            errors.Add("Invalid type for canvas background color: expected: Color, got: " + backgroundColorType);
        }
        var vTable = scope.vTable;
        var bound = vTable.Bind("canvas");

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
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitCompound(Compound node, Scope scope)
    {
        node.Scope = scope;
        var returnType = node.Statement1?.Accept(this, scope);
        var returnType2 = node.Statement2?.Accept(this, scope);
        return (returnType != GasType.Ok ? returnType : returnType2) ?? GasType.Ok;
    }

    /// <summary>
    /// Visits the if node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitIfStatement(If node, Scope scope)
    {
        var conditionType = node.Condition.Accept(this, scope);

        if(conditionType != GasType.Bool)
        {
            errors.Add("Invalid type for condition: expected: Boolean, got: " + conditionType);
            return GasType.Error;
        }

        scope = scope.EnterScope(node);

        var returnType = node.Statements?.Accept(this, scope);

        scope = scope.ExitScope();

        var @else = node.Else;

        if (@else is If @if)
        {
            returnType = @if.Accept(this, scope);
        }
        else if (@else != null)
        {
            scope = scope.EnterScope(@else);
            returnType = @else?.Accept(this, scope);
        }

        return returnType ?? GasType.Ok;
    }

    /// <summary>
    /// Visits the function call statement node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitFunctionCallStatement(FunctionCallStatement node, Scope scope)
    {
        node.Scope = scope;
        var identifier = node.Identifier;
        var function = scope.fTable.LookUp(identifier.Name);

        if (function == null)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name + " not found");
            return GasType.Error;
        }

        var parameters = node.Arguments.Select(expression => expression.Accept(this, scope)).ToList();

        if (parameters.Count != function?.Parameters.Count)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name +
                       " expecting arguments: \n" + function?.ParametersToString() +
                       "\n got arguments: \n" + parameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));
            return GasType.Error;
        }

        for (int i = 0; i < function?.Parameters.Count; i++)
        {
            if (function.Parameters[i].Type != parameters[i] && parameters[i] != GasType.Any && function.Parameters[i].Type != GasType.Any)
            {
                errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name +
                           " expecting arguments: \n" + function?.ParametersToString() +
                           "\n got arguments: \n" + parameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));
            }
        }

        return GasType.Ok;
    }

    /// <summary>
    /// Visits the while node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitWhile(While node, Scope scope)
    {
        scope = scope.EnterScope(node);
        var conditionType = node.Condition.Accept(this, scope);
        if(conditionType != GasType.Bool)
        {
            errors.Add("Invalid type for condition: expected: Boolean, got: " + conditionType);
        }
        var returnType = node.Statements?.Accept(this, scope);
        return returnType ?? GasType.Ok;
    }

    /// <summary>
    /// Visits the for node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitFor(For node, Scope scope)
    {
        scope = scope.EnterScope(node);
        var initializer = node.Initializer?.Accept(this, scope);

        if (initializer != GasType.Ok && initializer != GasType.Error)
        {
            errors.Add("Invalid type for initializer: expected: Ok, got: " + initializer);
        }

        var incrementer = node.Incrementer.Accept(this, scope);

        if (incrementer != GasType.Ok && incrementer != GasType.Error)
        {
            errors.Add("Invalid type for incrementer: expected: Ok, got: " + incrementer);
        }

        var condition = node.Condition.Accept(this, scope);

        if(condition != GasType.Bool)
        {
            errors.Add("Invalid type for condition: expected: Boolean, got: " + condition);
        }

        var returnType = node.Statements?.Accept(this, scope);

        return returnType ?? GasType.Ok;
    }

    /// <summary>
    /// Visit the return node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitReturn(Return node, Scope scope)
    {
        node.Scope = scope;
        return node.Expression.Accept(this, scope);
    }

    /// <summary>
    /// Visits the assignment node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
     public GasType VisitAssignment(Assignment node, Scope scope)
    {
        node.Scope = scope;
        var identifier = node.Identifier;
        var variableIndex = scope.vTable.LookUp(identifier.Name);
        var variable = scope.stoTable.LookUp(variableIndex.Value);
        if (variable == null)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: " + identifier + " not found");
            return GasType.Error;
        }
        var expressionType = node.Expression.Accept(this, scope);

        switch (node.Operator)
        {
            case "+=":
            case "-=":
            case "*=":
            case "/=":
                if (variable.Type != expressionType || variable.Type != GasType.Num)
                {
                    errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + variable.Type + " got: " + expressionType);
                }
                break;
            case "=":
                if (variable.Type != expressionType)
                {
                    errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + variable.Type + " got: " + expressionType);
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
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitDeclaration(Declaration node, Scope scope)
    {
        node.Scope = scope;
        var identifier = node.Identifier;
        var variable = scope.vTable.LookUp(identifier.Name);
        var type = node.Type.Accept(this, scope);

        if(variable != null)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: " + identifier.Name + " Can not redeclare variable");
            return GasType.Error;
        }

        var expression = node.Expression?.Accept(this, scope);

        if (expression != null && expression != GasType.Any && type != expression)
        {
            errors.Add("Line: " + node.LineNum + " Invalid type for variable: " + identifier.Name + " expected: " + type + " got: " + expression);
            return GasType.Error;
        }

        var storeIndex = scope.stoTable.AddNewValue(new Variable(identifier.Name, node.Scope, type, node.Expression));
        var bound = scope.vTable.Bind(identifier.Name, storeIndex);

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
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitIncrement(Increment increment, Scope scope)
    {
        var identifier = increment.Identifier;
        var op = increment.Operator;
        var variableIndex = scope.vTable.LookUp(identifier.Name);
        var variable = scope.stoTable.LookUp(variableIndex.Value);

        if(variable == null)
        {
            errors.Add("Line: " + increment.LineNum + " Variable name: " + identifier.Name + " not found");
            return GasType.Error;
        }

        switch (op)
        {
            case "++":
            case "--":
                if (variable.Type != GasType.Num)
                {
                    errors.Add("Invalid type for variable: " + identifier.Name + " expected: Num, got: " + variable.Type);
                }

                break;
        }

        return GasType.Ok;
    }

    /// <summary>
    /// Visit the function declaration node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitFunctionDeclaration(FunctionDeclaration node, Scope scope)
    {
        var identifier = node.Identifier.Name;
        scope = scope.EnterScope(node);

        var parameters = node.Parameters.Select(para =>
        {
            var name = para.Identifier.Name;
            var type = para.Type.Accept(this, scope);
            var bound = scope.vTable.Bind(name);
            if (!bound)
            {
                errors.Add("Line: " + node.LineNum + " Variable name: " + name + " already exists");
            }
            var variable = new Variable(name, scope, type,null);
            return variable;
        }).ToList();

        var statements = node.Statements;
        var returnType = node.Statements?.Accept(this, scope);
        var expectedReturnType = node.ReturnType.Accept(this, scope);

        if(expectedReturnType != returnType && expectedReturnType != GasType.Void)
        {
            errors.Add("Line: " + node.LineNum + " Invalid return type for function: " + identifier + " expected: " + expectedReturnType + " got: " + returnType);
            return GasType.Error;
        }

        var bound = scope.ParentScope?.fTable.Bind(identifier, new Function(parameters, expectedReturnType, statements, scope));
        if (bound == null || bound == false)
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
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitNum(Num node, Scope scope)
    {
        node.Scope = scope;
        return GasType.Num;
    }

    /// <summary>
    /// Visit the boolean node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitBoolean(Boolean node, Scope scope)
    {
        node.Scope = scope;
        return GasType.Bool;
    }

    /// <summary>
    /// Visit the string node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitString(String node, Scope scope)
    {
        node.Scope = scope;
        return GasType.String;
    }

    /// <summary>
    /// Visit the identifier node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitIdentifier(Identifier node, Scope scope)
    {
        node.Scope = scope;
        var variableIndex = scope.vTable.LookUp(node.Name);
        var variable = scope.stoTable.LookUp(variableIndex.Value);
        if(variable == null){
            errors.Add("Line: " + node.LineNum + " Variable name: " + node.Name + " not found");
            return GasType.Error;
        }
        return variable.Type;
    }

    /// <summary>
    /// Visit the unary operation node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitUnaryOp(UnaryOp node, Scope scope)
    {
        node.Scope = scope;
        var expression = node.Expression?.Accept(this, scope);
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
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitBinaryOp(BinaryOp node, Scope scope)
    {
        node.Scope = scope;
        var @operator = node.Op;
        var left = node.Left.Accept(this, scope);
        var right = node.Right.Accept(this, scope);

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
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitType(Type node, Scope scope)
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
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitFunctionCallTerm(FunctionCallTerm node, Scope scope)
    {
        node.Scope = scope;
        var identifier = node.Identifier;
        var function = scope.fTable.LookUp(identifier.Name);

        if (function == null)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name + " not found");
            return GasType.Error;
        }

        var parameters = node.Arguments.Select(expression => expression.Accept(this, scope)).ToList();
        if (parameters.Count != function?.Parameters.Count)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name +
                       " expecting arguments: \n" + function?.ParametersToString() +
                       "\n got arguments: \n" + parameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));
            return GasType.Error;
        }

        for (int i = 0; i < function?.Parameters.Count; i++)
        {
            if (function.Parameters[i].Type != GasType.Any && parameters[i] != GasType.Any && function.Parameters[i].Type != parameters[i])
            {
                errors.Add("Line: " + node.LineNum + " Function name: " + identifier.Name +
                           " expecting arguments: \n" + function?.ParametersToString() +
                           "\n got arguments: \n" + parameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));

            }
        }

        return function?.ReturnType ?? GasType.Error;
    }

    /// <summary>
    /// Visit the null node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitNull(Null node, Scope scope)
    {
        node.Scope = scope;
        return GasType.Null;
    }

    /// <summary>
    /// Visit the group node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitGroup(Group node, Scope scope)
    {
        scope = scope.EnterScope(node);
        var point = node.Point.Accept(this, scope);
        if (point != GasType.Point)
        {
            errors.Add("Invalid type for point: expected: Point, got: " + point);
            return GasType.Error;
        }

        node.Statements?.Accept(this, scope);
        return GasType.Group;
    }

    /// <summary>
    /// Visit the list node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public GasType VisitList(List node, Scope scope)
    {
        scope = scope.EnterScope(node);
        var list = node.Expressions.Select(expression => expression.Accept(this, scope)).ToList();
        var listType = node.Type.Accept(this,scope);
        return list.All(l => l == listType) ? listType : GasType.Error;
    }

    public GasType VisitSkip(Skip node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitAddToList(AddToList addToList, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitLine(SegLine node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitText(Text node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitCircle(Circle node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitRectangle(Rectangle node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitPoint(Point node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitColor(Color node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitSquare(Square node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitEllipse(Ellipse node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitSegLine(SegLine node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitLine(Line node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitGetFromList(GetFromList node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitRemoveFromList(RemoveFromList node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitArrow(Arrow node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitLengthOfList(LengthOfList node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitPolygon(Polygon polygon, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitTriangle(Triangle triangle, Scope scope)
    {
        throw new NotImplementedException();
    }
}
