//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Users/thomas/Documents/GASLanguageProcessor/GASLanguageProcessor/Frontend/GAS.g4 by ANTLR 4.13.1

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
	/// Visit a parse tree produced by <see cref="GASParser.functionDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionDeclaration([NotNull] GASParser.FunctionDeclarationContext context);
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
	/// Visit a parse tree produced by <see cref="GASParser.allTypes"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAllTypes([NotNull] GASParser.AllTypesContext context);
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
	/// Visit a parse tree produced by <see cref="GASParser.identifierTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdentifierTerm([NotNull] GASParser.IdentifierTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.numTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumTerm([NotNull] GASParser.NumTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.boolTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolTerm([NotNull] GASParser.BoolTermContext context);
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
	/// Visit a parse tree produced by <see cref="GASParser.squareTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSquareTerm([NotNull] GASParser.SquareTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.polygonTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPolygonTerm([NotNull] GASParser.PolygonTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.circleTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCircleTerm([NotNull] GASParser.CircleTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.rectangleTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRectangleTerm([NotNull] GASParser.RectangleTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.textTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTextTerm([NotNull] GASParser.TextTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] GASParser.FunctionCallContext context);
}
