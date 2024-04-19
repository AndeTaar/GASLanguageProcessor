using GASLanguageProcessor.AST.Statements;

namespace GASLanguageProcessor.AST.Expressions;

public class FunctionCall : Expression
{
    
    public AstNode Function { get; protected set; }
    public List<Expression> Arguments { get; protected set; }
    
    public FunctionCall(AstNode function, List<Expression> arguments)
    {
        Function = function;
        Arguments = arguments;
    }
    
    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Console.WriteLine(indent + this.GetType().Name);
        Function.Accept(visitor, indent + "   ");
        foreach (var argument in Arguments)
        {
            argument.Accept(visitor, indent + "   ");
        }
        return this;
    }
}