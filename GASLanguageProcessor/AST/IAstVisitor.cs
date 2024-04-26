using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;
using Boolean = GASLanguageProcessor.AST.Expressions.Boolean;
using String = GASLanguageProcessor.AST.Terms.String;
using Type = GASLanguageProcessor.AST.Terms.Type;

namespace GASLanguageProcessor.AST;

public interface IAstVisitor<T>
{
    T VisitText(Text node, Scope scope);

    T VisitBinaryOp(BinaryOp node, Scope scope);

    T VisitCircle(Circle node, Scope scope);

    T VisitColour(Colour node, Scope scope);

    T VisitGroup(Group node, Scope scope);

    T VisitNumber(Number node, Scope scope);

    T VisitPoint(Point node, Scope scope);

    T VisitRectangle(Rectangle node, Scope scope);

    T VisitSquare(Square node, Scope scope);

    T VisitLine(Line node, Scope scope);

    T VisitIfStatement(If node, Scope scope);

    T VisitBoolean(Boolean node, Scope scope);

    T VisitIdentifier(Identifier node, Scope scope);

    T VisitCompound(Compound node, Scope scope);

    T VisitAssignment(Assignment node, Scope scope);

    T VisitDeclaration(Declaration node, Scope scope);

    T VisitCanvas(Canvas node, Scope scope);

    T VisitWhile(While node, Scope scope);

    T VisitFor(For node, Scope scope);

    T VisitSkip(Skip node, Scope scope);

    T VisitUnaryOp(UnaryOp node, Scope scope);

    T VisitString(String s, Scope scope);

    T VisitType(Type type, Scope scope);

    T VisitFunctionDeclaration(FunctionDeclaration functionDeclaration, Scope scope);

    T VisitFunctionCall(FunctionCall functionCall, Scope scope);

    T VisitReturn(Return @return, Scope scope);

    T VisitNull(Null @null, Scope scope);
}
