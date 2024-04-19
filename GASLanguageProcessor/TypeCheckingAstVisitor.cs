using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using Boolean = GASLanguageProcessor.AST.Expressions.Boolean;
using String = GASLanguageProcessor.AST.Terms.String;
using Type = GASLanguageProcessor.AST.Terms.Type;

namespace GASLanguageProcessor;

public class TypeCheckingAstVisitor : IAstVisitor<GasType>
{
    //Everything is global scope for now
    private Dictionary<string, GasType> vTable = new Dictionary<string, GasType>();

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
        throw new System.NotImplementedException();
    }

    public GasType VisitColour(Colour node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitGroup(Group node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitNumber(Number node)
    {
        return GasType.Number;
    }

    public GasType VisitPoint(Point node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitRectangle(Rectangle node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitSquare(Square node)
    {
        throw new System.NotImplementedException();
    }

    public GasType VisitLine(Line node)
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }

    public GasType VisitCompound(Compound node)
    {
        var left = node.Statement1.Accept(this);
        var right = node.Statement2.Accept(this);

        return right;
    }

    public GasType VisitAssignment(Assignment node)
    {
        var left = node.Identifier;
        var right = node.Value.Accept(this);

        //Lookup left in variable table and check if right is the same type

        return right;
    }

    public GasType VisitDeclaration(Declaration node)
    {
        var identifier = node.Identifier;
        var type = node.Type.Accept(this);

        vTable.Add(identifier, type);

        //Add to variable table

        throw new System.NotImplementedException();
    }

    public GasType VisitCanvas(Canvas node)
    {
        var width = node.Width;
        var height = node.Height;
        var backgroundColourType = node.BackgroundColour.Accept(this);
        if(backgroundColourType != GasType.Colour)
        {
            throw new System.Exception("Invalid background colour type");
        }
        throw new System.NotImplementedException();
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
        throw new NotImplementedException();
    }

    public GasType VisitType(Type type)
    {
        switch (type.Value)
        {
            case "Number":
                return GasType.Number;
            case "Text":
                return GasType.Text;
        }
        throw new Exception(type.Value + " Not implemented");
    }

    public GasType VisitFunctionDeclaration(FunctionDeclaration functionDeclaration)
    {
        throw new NotImplementedException();
    }

    public GasType VisitFunctionCall(FunctionCall functionCall)
    {
        throw new NotImplementedException();
    }
}