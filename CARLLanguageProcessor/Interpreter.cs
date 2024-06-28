using System.Globalization;
using CARLLanguageProcessor.AST.Expressions;
using Boolean = CARLLanguageProcessor.AST.Expressions.Terms.Boolean;
using Expression = CARLLanguageProcessor.AST.Expressions.Expression;
using Num = CARLLanguageProcessor.AST.Expressions.Terms.Num;

namespace CARLLanguageProcessor;

public class Interpreter
{
    public List<string> errors = new();

    public object EvaluateProgram(AST.Expressions.Terms.Program program)
    {
        return EvaluateExpression(program.Expression);
    }

    public object EvaluateExpression(Expression expression)
    {
        switch (expression)
        {
            case Num num:
                return EvaluateLiterals(num);
            case Boolean boolean:
                return EvaluateLiterals(boolean);
            case UnaryOp unaryOp:
                var val = EvaluateExpression(unaryOp.Expression);
                return unaryOp.Op switch
                {
                    "-" => -(double)val,
                    "!" => !(bool)val,
                    _ => throw new NotImplementedException()
                };

            case BinaryOp binaryOp:
                var left = EvaluateExpression(binaryOp.Left);
                var right = EvaluateExpression(binaryOp.Right);
                if ((binaryOp.Op == "/" || binaryOp.Op == "%") && (double)right == 0)
                    throw new Exception("Division by zero is not allowed.");

                return binaryOp.Op switch
                {
                    "+" => (double)left + (double)right,
                    "-" => (double)left - (double)right,
                    "*" => (double)left * (double)right,
                    "/" => (double)left / (double)right,
                    "%" => (double)left % (double)right,
                    "==" => (double)left == (double)right,
                    "!=" => (double)left != (double)right,
                    "<" => (double)left < (double)right,
                    ">" => (double)left > (double)right,
                    "<=" => (double)left <= (double)right,
                    ">=" => (double)left >= (double)right,
                    "&&" => (bool)left && (bool)right,
                    "||" => (bool)left || (bool)right,
                    _ => throw new NotImplementedException()
                };
        }

        return null;
    }

    public object? EvaluateLiterals(Expression expression)
    {
        switch (expression)
        {
            case Num num:
                return double.Parse(num.Value, CultureInfo.InvariantCulture);
            case Boolean boolean:
                return bool.Parse(boolean.Value);
            default:
                throw new NotImplementedException();
        }
    }
}
