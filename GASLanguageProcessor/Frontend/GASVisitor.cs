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
	/// Visit a parse tree produced by <see cref="GASParser.simpleStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleStatement([NotNull] GASParser.SimpleStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.complexStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComplexStatement([NotNull] GASParser.ComplexStatementContext context);
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
	/// Visit a parse tree produced by <see cref="GASParser.elseStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseStatement([NotNull] GASParser.ElseStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.whileStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileStatement([NotNull] GASParser.WhileStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.forStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForStatement([NotNull] GASParser.ForStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.returnStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnStatement([NotNull] GASParser.ReturnStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.classDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassDeclaration([NotNull] GASParser.ClassDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.functionDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionDeclaration([NotNull] GASParser.FunctionDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitType([NotNull] GASParser.TypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.collectionType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCollectionType([NotNull] GASParser.CollectionTypeContext context);
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
	/// Visit a parse tree produced by <see cref="GASParser.listAccessExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListAccessExpression([NotNull] GASParser.ListAccessExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTerm([NotNull] GASParser.TermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.listTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListTerm([NotNull] GASParser.ListTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.groupTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroupTerm([NotNull] GASParser.GroupTermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="GASParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] GASParser.FunctionCallContext context);
}
