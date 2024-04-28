using System;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.TableType;
using Expression = GASLanguageProcessor.AST.Expressions.Expression;

namespace GASLanguageProcessor;

public class Interpreter
{
    public void EvaluateStatement(Statement statement, Scope scope)
    {
        switch (statement)
        {
            case Compound compound:
                EvaluateStatement(compound.Statement1, compound.Scope ?? scope);
                EvaluateStatement(compound.Statement2, compound.Scope ?? scope);
                return;
            case FunctionDeclaration functionDeclaration:
                EvaluateStatement(functionDeclaration.Statements, functionDeclaration.Scope ?? scope);
            return;
            case Declaration declaration:
                var val = EvaluateExpression(declaration.Expression, declaration.Scope ?? scope);
                var identifier = declaration.Identifier.Name;
                var variable = scope.vTable.LookUp(identifier);
                if (variable == null)
                {
                    throw new Exception($"Variable {identifier} not found");
                }
                variable.Value = val;
                return;
        }
    }

    public Term EvaluateExpression(Expression expression, Scope scope)
    {
        switch (expression)
        {
            case FunctionCall functionCall:
                Function function = scope.fTable.LookUp(functionCall.Identifier.Name);
                if (function == null)
                {
                    throw new Exception($"Function {functionCall.Identifier.Name} not found");
                }
                var functionScope = functionCall.Scope;
                for (var i = 0; i < functionCall.Arguments.Count; i++)
                {
                    var val = EvaluateExpression(functionCall.Arguments[i], functionScope);
                    var identifier = function.Parameters[i].Identifier;
                    var type = function.Parameters[i].Type;
                    functionScope.vTable.Bind(identifier, new Variable(identifier, type, val));
                }
                EvaluateStatement(function.Statements, functionScope);
                return null;
            case BinaryOp binaryOp:
                var left = EvaluateExpression(binaryOp.Left, binaryOp.Scope ?? scope);
                var right = EvaluateExpression(binaryOp.Right, binaryOp.Scope ?? scope);
                return binaryOp.Op switch
                {
                    "+" => (dynamic)left + (dynamic)right,
                    "-" => (dynamic)left - (dynamic)right,
                    "*" => (dynamic)left * (dynamic)right,
                    "/" => (dynamic)left / (dynamic)right,
                    _ => throw new NotImplementedException()
                };

            case Identifier identifier:
                return EvaluateExpression(scope.vTable.LookUp(identifier.Name)?.Value, scope);

            case Number number:
                return number;
        }

        return null;
    }
}