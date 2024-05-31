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
    T VisitProgram(Expressions.Terms.Program node, TypeEnv envT);

    T VisitBinaryOp(BinaryOp node, TypeEnv envT);

    T VisitGroup(Group node, TypeEnv envT);

    T VisitList(List node, TypeEnv envT);

    T VisitNum(Num node, TypeEnv envT);

    T VisitIfStatement(If node, TypeEnv envT);

    T VisitBoolean(Boolean node, TypeEnv envT);

    T VisitIdentifier(Identifier node, TypeEnv envT);

    T VisitCompound(Compound node, TypeEnv envT);

    T VisitAssignment(Assignment node, TypeEnv envT);

    T VisitDeclaration(Declaration node, TypeEnv envT);

    T VisitCanvas(Canvas node, TypeEnv envT);

    T VisitWhile(While node, TypeEnv envT);

    T VisitFor(For node, TypeEnv envT);

    T VisitSkip(Skip node, TypeEnv envT);

    T VisitUnaryOp(UnaryOp node, TypeEnv envT);

    T VisitString(String node, TypeEnv envT);

    T VisitType(Type node, TypeEnv envT);

    T VisitFunctionDeclaration(FunctionDeclaration node, TypeEnv envT);

    T VisitFunctionCallStatement(FunctionCallStatement node, TypeEnv envT);

    T VisitFunctionCallTerm(FunctionCallTerm node, TypeEnv envT);

    T VisitReturn(Return node, TypeEnv envT);

    T VisitNull(Null node, TypeEnv envT);

    T VisitText(Text node, TypeEnv envT);

    T VisitCircle(Circle node, TypeEnv envT);

    T VisitRectangle(Rectangle node, TypeEnv envT);

    T VisitPoint(Point node, TypeEnv envT);

    T VisitColor(Color node, TypeEnv envT);

    T VisitSquare(Square node, TypeEnv envT);

    T VisitAddToList(AddToList node, TypeEnv envT);

    T VisitEllipse(Ellipse node, TypeEnv envT);

    T VisitSegLine(SegLine node, TypeEnv envT);

    T VisitLine(Line node, TypeEnv envT);

    T VisitGetFromList(GetFromList node, TypeEnv envT);

    T VisitRemoveFromList(RemoveFromList node, TypeEnv envT);

    T VisitArrow(Arrow node, TypeEnv envT);

    T VisitLengthOfList(LengthOfList node, TypeEnv envT);

    T VisitPolygon(Polygon node, TypeEnv envT);

    T VisitTriangle(Triangle node, TypeEnv envT);

    T VisitIncrement(Increment node, TypeEnv envT);

    T VisitStructDeclaration(StructDeclaration node, TypeEnv envT);

    T VisitStructAssignment(StructAssignment node, TypeEnv envT);

    T VisitStructCreation(StructCreation node, TypeEnv envT);
}
