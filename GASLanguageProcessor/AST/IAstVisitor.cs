﻿using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.TableType;
using Boolean = GASLanguageProcessor.AST.Expressions.Terms.Boolean;
using String = GASLanguageProcessor.AST.Expressions.Terms.String;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST;

public interface IAstVisitor<T>
{
    T VisitBinaryOp(BinaryOp node);

    T VisitGroup(Group node);

    T VisitList(List node);

    T VisitNumber(Number node);

    T VisitIfStatement(If node);

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

    T VisitFunctionCallStatement(FunctionCallStatement functionCallStatement);

    T VisitFunctionCallTerm(FunctionCallTerm functionCallTerm);

    T VisitReturn(Return @return);

    T VisitNull(Null @null);

    T VisitText(Text text);

    T VisitCircle(Circle circle);

    T VisitRectangle(Rectangle rectangle);

    T VisitPoint(Point point);

    T VisitColor(Color color);

    T VisitSquare(Square square);

    T VisitAddToList(AddToList addToList);

    T VisitCollectionDeclaration(CollectionDeclaration collectionDeclaration);

    T VisitEllipse(Ellipse ellipse);

    T VisitSegLine(SegLine segLine);

    T VisitLine(Line line);
}
