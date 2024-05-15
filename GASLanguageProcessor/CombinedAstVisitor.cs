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

    public GasType VisitList(List node, Scope scope)
    {
        scope = scope.EnterScope(node);
        var list = node.Expressions.Select(expression => expression.Accept(this, scope)).ToList();
        var listType = node.Type.Accept(this,scope);
        return list.All(l => l == listType) ? listType : GasType.Error;
    }

    public GasType VisitAddToList(AddToList addToList, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitCollectionDeclaration(CollectionDeclaration node, Scope scope)
    {
        node.Scope = scope;
        var identifier = node.Identifier;
        var variable = scope.LookupAttribute(identifier, scope, scope, errors);
        if(variable != null)
        {
            errors.Add("Line: " + node.LineNum + " variable name: " + identifier.Name + " Can not redeclare variable");
            return GasType.Error;
        }
        var type = node.Type.Accept(this, scope);
        var expression = node.Expression?.Accept(this, scope);

        if (type != expression && expression != null)
        {
            errors.Add("Line: " + node.LineNum + " Invalid type for variable: " + identifier.Name + " expected: " + type + " got: " + expression);
            return GasType.Error;
        }

        var bound = scope.vTable.Bind(identifier.Name, new Variable(identifier.Name, node.Scope, type, node.Expression));

        if(!bound){
            errors.Add("Line: " + node.LineNum + " Variable name: " + identifier.Name + " already exists");
            return GasType.Error;
        }

        return type;
    }

    public GasType VisitNum(Num node, Scope scope)
    {
        node.Scope = scope;
        return GasType.Num;
    }

    public GasType VisitIfStatement(If node, Scope scope)
    {
        var condition = node.Condition.Accept(this, scope);
        scope = scope.EnterScope(node);
        node.Statements?.Accept(this, scope);
        scope = scope.ExitScope();
        var @else = node.Else;
        if (@else is If @if)
        {
            @if.Accept(this, scope);
        }
        else if (@else != null)
        {
            scope = scope.EnterScope(@else);
            @else?.Accept(this, scope);
        }

        if (condition != GasType.Bool)
        {
            errors.Add("Invalid type for condition: expected: Boolean, got: " + condition);
            return GasType.Error;
        }

        return GasType.Error;
    }

    public GasType VisitBoolean(Boolean node, Scope scope)
    {
        node.Scope = scope;
        return GasType.Bool;
    }

    public GasType VisitIdentifier(Identifier node, Scope scope)
    {
        node.Scope = scope;
        var variable = scope.LookupAttribute(node, scope, scope, errors);
        if(variable == null){
            errors.Add("Line: " + node.LineNum + " Variable name: " + node.Name + " not found");
            return GasType.Error;
        }
        return variable.Type;
    }

    public GasType VisitCompound(Compound node, Scope scope)
    {
        node.Scope = scope;
        node.Statement1?.Accept(this, scope);
        node.Statement2?.Accept(this, scope);
        return GasType.Error;
    }

    public GasType VisitAssignment(Assignment node, Scope scope)
    {
        node.Scope = scope;
        var identifier = node.Identifier;
        var variable = scope.LookupAttribute(identifier, scope, scope, errors);
        if (variable == null)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: " + identifier + " not found");
            return GasType.Error;
        }
        var expression = node.Expression.Accept(this, scope);

        switch (node.Operator)
        {
            case "+=":
                if (variable.Type == expression && variable.Type == GasType.Num)
                {
                    return GasType.Num;
                }
                errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + variable.Type + " got: " + expression);
                break;
            case "-=":
                if (variable.Type == expression && variable.Type == GasType.Num)
                {
                    return GasType.Num;
                }
                errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + variable.Type + " got: " + expression);
                break;
            case "*=":
                if (variable.Type == expression && variable.Type == GasType.Num)
                {
                    return GasType.Num;
                }
                errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + variable.Type + " got: " + expression);
                break;
            case "/=":
                if (variable.Type == expression && variable.Type == GasType.Num)
                {
                    return GasType.Num;
                }
                errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + variable.Type + " got: " + expression);
                break;
            case "=":
                if (variable.Type == expression)
                {
                    return variable.Type;
                }
                errors.Add("Invalid type for variable: " + identifier.Name + " expected: " + variable.Type + " got: " + expression);
                break;
            default:
                errors.Add("Invalid operator: " + node.Operator);
                break;
        }

        return variable.Type;
    }

    public GasType VisitDeclaration(Declaration node, Scope scope)
    {
        node.Scope = scope;
        var identifier = node.Identifier;
        var variable = scope.LookupAttribute(identifier, scope, scope, errors);
        if(variable != null)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: " + identifier.Name + " Can not redeclare variable");
            return GasType.Error;
        }
        var type = node.Type.Accept(this, scope);
        var expression = node.Expression?.Accept(this, scope);
        if (expression != GasType.Any && type != expression && expression != null)
        {
            errors.Add("Line: " + node.LineNum + " Invalid type for variable: " + identifier.Name + " expected: " + type + " got: " + expression);
            return GasType.Error;
        }
        var bound = scope.vTable.Bind(identifier.Name, new Variable(identifier.Name, node.Scope, type, node.Expression));

        if (!bound)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: " + identifier.Name + " already exists");
            return GasType.Error;
        }
        return type;
    }

    public GasType VisitCanvas(Canvas node, Scope scope)
    {
        node.Scope = scope;
        var width = node.Width.Accept(this, scope);
        var height = node.Height.Accept(this, scope);
        var backgroundColor = node.BackgroundColor?.Accept(this, scope);
        var bound = scope.vTable.Bind("canvas", new Variable("canvas", node));

        if (!bound)
        {
            errors.Add("Line: " + node.LineNum + " Variable name: canvas already exists");
            return GasType.Error;
        }

        if(width != GasType.Num || height != GasType.Num || backgroundColor != GasType.Color)
        {
            errors.Add("Invalid types for canvas: expected: Num, Num, Color, got: " + width + ", " + height + ", " + backgroundColor);
            return GasType.Error;
        }
        return GasType.Canvas;
    }

    public GasType VisitWhile(While node, Scope scope)
    {
        scope = scope.EnterScope(node);
        var condition = node.Condition.Accept(this, scope);
        if(condition != GasType.Bool)
        {
            errors.Add("Invalid type for condition: expected: Boolean, got: " + condition);
        }
        node.Statements?.Accept(this, scope);
        return condition;
    }

    public GasType VisitFor(For node, Scope scope)
    {
        scope = scope.EnterScope(node);
        var declaration = node.Declaration?.Accept(this, scope);
        var assignment = node.Assignment?.Accept(this, scope);
        var increment = node.Increment.Accept(this, scope);
        var condition = node.Condition.Accept(this, scope);
        var body = node.Statements?.Accept(this, scope);
        if(condition != GasType.Bool)
        {
            errors.Add("Invalid type for condition: expected: Boolean, got: " + condition);
        }

        return GasType.Error;
    }

    public GasType VisitSkip(Skip node, Scope scope)
    {
        throw new NotImplementedException();
    }

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

    public GasType VisitString(String node, Scope scope)
    {
        node.Scope = scope;
        return GasType.String;
    }

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
        }
        errors.Add(node.Value + " Not implemented");
        return GasType.Error;
    }

    public GasType VisitFunctionDeclaration(FunctionDeclaration node, Scope scope)
    {
        var identifier = node.Identifier.Name;
        scope = scope.EnterScope(node);
        var parameters = node.Declarations.Select(decl =>
        {
            var variable = new Variable(decl.Identifier.Name, scope, decl.Accept(this, scope),
                decl.Expression);
            return variable;
        }).ToList();
        var statements = node.Statements;
        var statementsInScope = node.Statements?.Accept(this, scope);
        var type = node.ReturnType.Accept(this, scope);
        var bound = scope.ParentScope?.fTable.Bind(identifier, new Function(parameters, type, statements, scope));
        if (bound == null || bound == false)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier + " already exists");
            return GasType.Error;
        }

        return GasType.Error;
    }

    public GasType VisitFunctionCallStatement(FunctionCallStatement node, Scope scope)
    {
        node.Scope = scope;
        var functionAndIdentifier = scope.LookupMethod(node.Identifier, scope, scope, errors);
        var identifier = functionAndIdentifier.Item1;
        var function = functionAndIdentifier.Item2;

        if (function == null)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.ToCompoundIdentifierName() + " not found");
            return GasType.Error;
        }

        var parameters = node.Arguments.Select(expression => expression.Accept(this, scope)).ToList();
        if (parameters.Count != function?.Parameters.Count)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.ToCompoundIdentifierName() +
                       " expecting arguments: \n" + function?.ParametersToString() +
                       "\n got arguments: \n" + parameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));
            return GasType.Error;
        }

        for (int i = 0; i < function?.Parameters.Count; i++)
        {
            if (function.Parameters[i].Type != parameters[i] && parameters[i] != GasType.Any && function.Parameters[i].Type != GasType.Any)
            {
                errors.Add("Line: " + node.LineNum + " Function name: " + identifier.ToCompoundIdentifierName() +
                           " expecting arguments: \n" + function?.ParametersToString() +
                           "\n got arguments: \n" + parameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));
            }
        }

        return function?.ReturnType ?? GasType.Error;
    }

    public GasType VisitFunctionCallTerm(FunctionCallTerm node, Scope scope)
    {
        node.Scope = scope;
        var functionAndIdentifier = scope.LookupMethod(node.Identifier, scope, scope, errors);
        var identifier = functionAndIdentifier.Item1;
        var function = functionAndIdentifier.Item2;

        if (function == null)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.ToCompoundIdentifierName() + " not found");
            return GasType.Error;
        }

        var parameters = node.Arguments.Select(expression => expression.Accept(this, scope)).ToList();
        if (parameters.Count != function?.Parameters.Count)
        {
            errors.Add("Line: " + node.LineNum + " Function name: " + identifier.ToCompoundIdentifierName() +
                       " expecting arguments: \n" + function?.ParametersToString() +
                       "\n got arguments: \n" + parameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));
            return GasType.Error;
        }

        for (int i = 0; i < function?.Parameters.Count; i++)
        {
            if (function.Parameters[i].Type != GasType.Any && parameters[i] != GasType.Any && function.Parameters[i].Type != parameters[i])
            {
                errors.Add("Line: " + node.LineNum + " Function name: " + identifier.ToCompoundIdentifierName() +
                           " expecting arguments: \n" + function?.ParametersToString() +
                            "\n got arguments: \n" + parameters.Select(p => p.ToString()).Aggregate((a, b) => a + ", " + b));

            }
        }

        return function?.ReturnType ?? GasType.Error;
    }

    public GasType VisitReturn(Return node, Scope scope)
    {
        node.Scope = scope;
        return node.Expression.Accept(this, scope);
    }

    public GasType VisitNull(Null node, Scope scope)
    {
        node.Scope = scope;
        return GasType.Null;
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

    public GasType VisitLengthOfList(LengthOfList node, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitPolygon(Polygon polygon, Scope scope)
    {
        throw new NotImplementedException();
    }

    public GasType VisitIncrement(Increment increment, Scope scope)
    {
        var identifier = increment.Identifier;
        var op = increment.Operator;
        var variable = scope.LookupAttribute(identifier, scope, scope, errors);

        if(variable == null){
            errors.Add("Line: " + increment.LineNum + " Variable name: " + identifier.Name + " not found");
            return GasType.Error;
        }

        switch (op)
        {
            case "++":
                if(variable.Type == GasType.Num)
                    return GasType.Num;
                errors.Add("Invalid type for variable: " + identifier.Name + " expected: Num, got: " + variable.Type);
                break;
            case "--":
                if(variable.Type == GasType.Num)
                    return GasType.Num;
                errors.Add("Invalid type for variable: " + identifier.Name + " expected: Num, got: " + variable.Type);
                break;
        }

        return GasType.Error;
    }

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

                errors.Add("Invalid types for binary operation: " + @operator + " expected: String or Num, got: " + left + " and " + right);
                return GasType.Error;

            case "-" or "*" or "/" or "%":
                if (left == GasType.Num && right == GasType.Num)
                {
                    node.Type = GasType.Num;
                    return GasType.Num;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Num, got: " + left + " and " + right);
                return GasType.Error;

            case "<" or ">" or "<=" or ">=":
                if (left == GasType.Num && right == GasType.Num)
                {
                    node.Type = GasType.Bool;
                    return GasType.Bool;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Num, got: " + left + " and " + right);
                return GasType.Error;

            case "&&" or "||":
                if (left == GasType.Bool && right == GasType.Bool)
                {
                    node.Type = GasType.Bool;
                    return GasType.Bool;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Boolean, got: " + left + " and " + right);
                return GasType.Error;

            case "==" or "!=":
                if ((left == GasType.Bool && right == GasType.Bool) ||
                    (left == GasType.Num && right == GasType.Num))
                {
                    node.Type = GasType.Bool;
                    return GasType.Bool;
                }

                errors.Add("Invalid types for binary operation: " + @operator + " expected: Boolean, got: " + left + " and " + right);
                return GasType.Error;


            default:
                errors.Add("Invalid operator: " + @operator);
                return GasType.Error;
        }
    }
}
