using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.TableType;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST;

public interface IAstVisitor<T>
{
    T VisitBinaryOp(BinaryOp node, Scope scope);

    T VisitGroup(Group node, Scope scope);

    T VisitList(List node, Scope scope);

    T VisitNum(Num node, Scope scope);

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

    T VisitString(String node, Scope scope);

    T VisitType(Type node, Scope scope);

    T VisitFunctionDeclaration(FunctionDeclaration node, Scope scope);

    T VisitFunctionCallStatement(FunctionCallStatement node, Scope scope);

    T VisitFunctionCallTerm(FunctionCallTerm node, Scope scope);

    T VisitReturn(Return node, Scope scope);

    T VisitNull(Null node, Scope scope);

    T VisitText(Text node, Scope scope);

    T VisitCircle(Circle node, Scope scope);

    T VisitRectangle(Rectangle node, Scope scope);

    T VisitPoint(Point node, Scope scope);

    T VisitColor(Color node, Scope scope);

    T VisitSquare(Square node, Scope scope);

    T VisitAddToList(AddToList node, Scope scope);

    T VisitCollectionDeclaration(CollectionDeclaration node, Scope scope);

    T VisitEllipse(Ellipse node, Scope scope);

    T VisitSegLine(SegLine node, Scope scope);

    T VisitLine(Line node, Scope scope);

    T VisitGetFromList(GetFromList node, Scope scope);

    T VisitRemoveFromList(RemoveFromList node, Scope scope);

    T VisitLengthOfList(LengthOfList node, Scope scope);

    T VisitPolygon(Polygon polygon, Scope scope);
}
