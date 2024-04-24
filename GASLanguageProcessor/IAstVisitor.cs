using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using Boolean = GASLanguageProcessor.AST.Expressions.Boolean;
using String = GASLanguageProcessor.AST.Terms.String;
using Type = GASLanguageProcessor.AST.Terms.Type;

namespace GASLanguageProcessor;

public interface IAstVisitor<T>
{
    T VisitText(Text node);

    T VisitBinaryOp(BinaryOp node);

    T VisitCircle(Circle node);

    T VisitColour(Colour node);

    T VisitGroup(Group node);

    T VisitNumber(Number node);

    T VisitPoint(Point node);

    T VisitRectangle(Rectangle node);

    T VisitSquare(Square node);

    T VisitLine(Line node);

    T VisitIfStatement(If node);

    T VisitElseStatement(Else node);

    T VisitBoolean(Boolean node);

    T VisitIdentifier(Identifier node);

    T VisitCompound(Compound node);

    T VisitAssignment(Assignment node);

    T VisitDeclaration(Declaration node);

    T VisitCanvas(Canvas node);

    T VisitWhile(While node);
    
    T VisitFor(For node);

    T VisitSkip(Skip node);

    T VisitUnaryOp(UnaryOp node);

    T VisitString(String s);

    T VisitType(Type type);

    T VisitFunctionDeclaration(FunctionDeclaration functionDeclaration);

    T VisitFunctionCall(FunctionCall functionCall);

    T VisitReturn(Return @return);


    T VisitNull(Null @null);
}