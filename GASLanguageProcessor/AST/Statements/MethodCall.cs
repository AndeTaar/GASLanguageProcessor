using GASLanguageProcessor.AST.Expressions;

namespace GASLanguageProcessor.AST.Statements;

public class MethodCall
{
    public string MethodName { get; set; }
    public List<Expression> Arguments { get; set; }
    
    public MethodCall(string methodName, List<Expression> arguments)
    {
        MethodName = methodName;
        Arguments = arguments;
    }
}