using CARLLanguageProcessor.AST.Expressions;
using CARLLanguageProcessor.AST.Expressions.Terms;
using CARLLanguageProcessor.AST.Expressions.Terms.Identifiers;
using CARLLanguageProcessor.AST.Statements;
using CARLLanguageProcessor.TableType;
using Array = CARLLanguageProcessor.AST.Expressions.Terms.Array;
using Boolean = CARLLanguageProcessor.AST.Expressions.Terms.Boolean;
using String = CARLLanguageProcessor.AST.Expressions.Terms.String;
using Type = CARLLanguageProcessor.AST.Expressions.Terms.Type;

namespace CARLLanguageProcessor.AST;

public interface IAstVisitor<T>
{
    T VisitProgram(Expressions.Terms.Program node, TypeEnv envT);

    T VisitBinaryOp(BinaryOp node, TypeEnv envT);

    T VisitGroup(Group node, TypeEnv envT);

    T VisitNum(Num node, TypeEnv envT);

    T VisitIfStatement(If node, TypeEnv envT);

    T VisitBoolean(Boolean node, TypeEnv envT);

    T VisitCompound(Compound node, TypeEnv envT);

    T VisitAssignment(Assignment node, TypeEnv envT);

    T VisitDeclaration(Declaration node, TypeEnv envT);

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

    T VisitAddToArray(AddToArray node, TypeEnv envT);

    T VisitGetFromArray(GetFromArray node, TypeEnv envT);

    T VisitLengthOfArray(SizeOfArray node, TypeEnv envT);

    T VisitIncrement(Increment node, TypeEnv envT);

    T VisitRecordDefinition(RecordDefinition node, TypeEnv envT);

    T VisitIdentifier(Identifier identifier, TypeEnv envT);

    T VisitRecord(Record record, TypeEnv envT);

    T VisitArray(Array array, TypeEnv envT);
}
