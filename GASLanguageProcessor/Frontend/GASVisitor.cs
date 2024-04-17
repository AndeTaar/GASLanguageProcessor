//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C://Users//danbo//source//repos//GASLanguageProcessor//GASLanguageProcessor//Frontend//GAS.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="GASParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public interface IGASVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] GASParser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.canvas"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCanvas([NotNull] GASParser.CanvasContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] GASParser.StatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclaration([NotNull] GASParser.DeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignment([NotNull] GASParser.AssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStatement([NotNull] GASParser.IfStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.whileStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileStatement([NotNull] GASParser.WhileStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.print"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrint([NotNull] GASParser.PrintContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.functionDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionDeclaration([NotNull] GASParser.FunctionDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.point"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoint([NotNull] GASParser.PointContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.rectangle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRectangle([NotNull] GASParser.RectangleContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.square"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSquare([NotNull] GASParser.SquareContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.circle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCircle([NotNull] GASParser.CircleContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.polygon"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPolygon([NotNull] GASParser.PolygonContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.text"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitText([NotNull] GASParser.TextContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLine([NotNull] GASParser.LineContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.pointDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPointDeclaration([NotNull] GASParser.PointDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.rectangleDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRectangleDeclaration([NotNull] GASParser.RectangleDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.squareDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSquareDeclaration([NotNull] GASParser.SquareDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.circleDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCircleDeclaration([NotNull] GASParser.CircleDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.polygonDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPolygonDeclaration([NotNull] GASParser.PolygonDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.textDecleration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTextDecleration([NotNull] GASParser.TextDeclerationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.lineDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLineDeclaration([NotNull] GASParser.LineDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.colourDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitColourDeclaration([NotNull] GASParser.ColourDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.stringDecleration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringDecleration([NotNull] GASParser.StringDeclerationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.collectionDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCollectionDeclaration([NotNull] GASParser.CollectionDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitList([NotNull] GASParser.ListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.groupDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroupDeclaration([NotNull] GASParser.GroupDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.listAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListAccess([NotNull] GASParser.ListAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.dataType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDataType([NotNull] GASParser.DataTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.allTypes"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAllTypes([NotNull] GASParser.AllTypesContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.string"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitString([NotNull] GASParser.StringContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.colour"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitColour([NotNull] GASParser.ColourContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] GASParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.equalityExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEqualityExpression([NotNull] GASParser.EqualityExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.relationExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRelationExpression([NotNull] GASParser.RelationExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.binaryExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinaryExpression([NotNull] GASParser.BinaryExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.multExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultExpression([NotNull] GASParser.MultExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.notExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNotExpression([NotNull] GASParser.NotExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.numTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumTerm([NotNull] GASParser.NumTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTerm([NotNull] GASParser.TermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.pointTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPointTerm([NotNull] GASParser.PointTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.colourTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitColourTerm([NotNull] GASParser.ColourTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.listTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListTerm([NotNull] GASParser.ListTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.stringTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringTerm([NotNull] GASParser.StringTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.lineTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLineTerm([NotNull] GASParser.LineTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] GASParser.FunctionCallContext context);
}
