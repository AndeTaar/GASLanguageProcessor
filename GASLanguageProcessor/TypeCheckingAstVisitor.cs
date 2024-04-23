using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;
using Boolean = GASLanguageProcessor.AST.Expressions.Boolean;
using String = GASLanguageProcessor.AST.Terms.String;
using Type = GASLanguageProcessor.AST.Terms.Type;

namespace GASLanguageProcessor;

public class TypeCheckingAstVisitor : IAstVisitor<GasType>
{
    /**Everything is global scope for now
    private VariableTable vTable = new VariableTable();
    private FunctionTable fTable = new FunctionTable();
    
    fTable and vTables are moved into the Scope class
    **/

    private Scope GlobalScope = new(null);
    
    public List<string> scopeErrors = new();
    public List<string> typeErrors = new();

    public GasType VisitText(Text node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitBinaryOp(BinaryOp node)
    {
        var operant = node.Op;
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        switch (operant)
        {
            case "+" or "-" or "*" or "/":
                if (left == GasType.Number && right == GasType.Number)
                {
                    return GasType.Number;
                }
                else
                {
                    throw new System.Exception("Invalid types for operant");
                }
        }

        throw new System.Exception("Invalid operant");
    }

    public GasType VisitCircle(Circle node)
    {
        return GasType.Circle;
    }

    public GasType VisitColour(Colour node)
    {
        return GasType.Colour;
    }

    public GasType VisitGroup(Group node)
    {
        node.Terms.ForEach(no => no.Accept(this));
        return GasType.Group;
    }

    public GasType VisitNumber(Number node)
    {
        return GasType.Number;
    }

    public GasType VisitPoint(Point node)
    {
        return GasType.Point;
    }

    public GasType VisitRectangle(Rectangle node)
    {
        return GasType.Rectangle;
    }

    public GasType VisitSquare(Square node)
    {
        return GasType.Square;
    }

    public GasType VisitLine(Line node)
    {
        return GasType.Line;
    }

    public GasType VisitIfStatement(If node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitBoolean(Boolean node)
    {
        return GasType.Boolean;
    }

    public GasType VisitIdentifier(Identifier node)
    {
        GasType type;
        try
        {
            type = Scope.MostRecent().GetVariable(node.Name).Type;
        }
        catch (Exception e)
        {
            scopeErrors.Add("Variable name: " + node.Name + " not found");
            return GasType.Error;
        }
        return type;
    }

    public GasType VisitCompound(Compound node)
    {
        var left = node.Statement1?.Accept(this);
        var right = node.Statement2?.Accept(this);

        if (left != null)
        {
            return left ?? GasType.Error;
        }

        if (right != null)
        {
            return right ?? GasType.Error;
        }

        return GasType.Error;
    }

    public GasType VisitAssignment(Assignment node)
    {
        var left = node.Identifier.Name;
        var type = node.Value.Accept(this);
        
        try
        {
            
            VariableType variable = Scope.MostRecent().GetVariable(left);
            if (variable.Type != type)
            {
                throw new Exception("Invalid assignment");
            }
        }
        catch (Exception e)
        {
            scopeErrors.Add("Variable name: " + node.Identifier.Name + " not found");
            return GasType.Error;
        }

        return type;
    }

    public GasType VisitDeclaration(Declaration node)
    {
        var identifier = node.Identifier.Name;
        var type = node.Type.Accept(this);
        var value = node.Value;
        var typeOfValue = value?.Accept(this);
        if (type != typeOfValue && typeOfValue != null)
        {
            typeErrors.Add("Invalid type for variable: " + identifier + " expected: " + type + " got: " + typeOfValue);
            return GasType.Error;
        }
        
        if (Scope.MostRecent().VtableContains(identifier))
        {
            scopeErrors.Add("Variable name: " + identifier + " is already declared elsewhere");
            return GasType.Error;
        }
        
        Scope.MostRecent().Variables.Add(identifier, new VariableType(type, value));
        return type;
    }

    public GasType VisitCanvas(Canvas node)
    {
        // Perchance a little scuffed (Canvas scope is effectively just Global Scope)
        Scope canvasScope = new(GlobalScope); 
        
        var width = node.Width;
        var height = node.Height;
        var backgroundColourType = node.BackgroundColour.Accept(this);
        if (backgroundColourType != GasType.Colour)
        {
            typeErrors.Add("Invalid background colour type");
            return GasType.Error;
        }

        return GasType.Canvas;
    }

    public GasType VisitWhile(While node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitSkip(Skip node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitUnaryOp(UnaryOp node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitString(String s)
    {
        return GasType.String;
    }

    public GasType VisitType(Type type)
    {
        switch (type.Value)
        {
            case "number":
                return GasType.Number;
            case "text":
                return GasType.Text;
            case "colour":
                return GasType.Colour;
            case "boolean":
                return GasType.Boolean;
            case "square":
                return GasType.Square;
            case "rectangle":
                return GasType.Rectangle;
            case "point":
                return GasType.Point;
            case "line":
                return GasType.Line;
            case "circle":
                return GasType.Circle;
        }
        typeErrors.Add(type.Value + " Not implemented");
        return GasType.Error;
    }

    public GasType VisitFunctionDeclaration(FunctionDeclaration functionDeclaration)
    {
        Scope parentScope = Scope.MostRecent();
        var returnType = functionDeclaration.ReturnType.Accept(this);

        var identifier = functionDeclaration.Identifier;
        
        if (parentScope.FtableContains(identifier.Name))
        {
            scopeErrors.Add("Function " + identifier + " already exists");
            return GasType.Error; //Crigne mais bon
        }
        
        var newScope = new Scope(parentScope);
        
        var declarations = functionDeclaration.Declarations;
        foreach (var declaration in declarations.Where(declaration => 
                     newScope.VtableContains(declaration.Identifier.Name)
                     || newScope.FtableContains(declaration.Identifier.Name)))
        {
            scopeErrors.Add("Declaration " + declaration.Identifier.Name + " already exists");
        }
        
        foreach (var declaration in declarations)
        {
            var type = declaration.Type.Accept(this); 
            var value = declaration.Value;

            // The declarations of the function are added to the new scope's vtable
            newScope.Variables.Add(declaration.Identifier.Name, new VariableType(type, value));
        }

        var parameterTypes = functionDeclaration.Declarations.Select(decl => decl.Accept(this)).ToList();
        
        // Add function to ftable in the its new scope
        newScope.Functions.Add(identifier.Name, new FunctionType(returnType, parameterTypes));
        
        return returnType;
    }

    public GasType VisitFunctionCall(FunctionCall functionCall)
    {
        var identifier = functionCall.Identifier;

        var parameterTypes = new List<GasType>();
        foreach (var parameter in functionCall.Parameters)
        {
            parameterTypes.Add(parameter.Accept(this));
        }

        if (!Scope.MostRecent().FtableContains(identifier.Name))
        {
            scopeErrors.Add("Function name: " + functionCall.Identifier.Name + " not found");
            return GasType.Error;
        }
        
        FunctionType type = Scope.MostRecent().GetFunction(identifier.Name);

        bool error = false;
        for (int i = 0; i < type.ParameterTypes.Count; i++)
        {
            if (type.ParameterTypes[i] != parameterTypes[i])
            {
                typeErrors.Add("Invalid parameter " + i + " for function: " + identifier.Name + " expected: " + type.ParameterTypes[i] + " got: " + parameterTypes[i]);
                error = true;
            }
        }

        if (error)
        {
            return GasType.Error;
        }

        return type.ReturnType;
    }
}