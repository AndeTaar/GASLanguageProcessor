using GASLanguageProcessor.AST.Expressions;

namespace GASLanguageProcessor.TableType;

public class Constructor
{
    public Constructor(List<String> parameters, List<Expression> arguments, Statement statements, VarEnv varEnv, FuncEnv funcEnv, Store store)
    {
        Parameters = parameters;
        Arguments = arguments;
        Statements = statements;
        VarEnv = varEnv;
        FuncEnv = funcEnv;
        Store = store;
    }

    public List<String> Parameters { get; protected set; }
    public List<Expression> Arguments { get; protected set; }
    public Statement Statements { get; protected set; }
    public VarEnv VarEnv { get; set; }
    public FuncEnv FuncEnv { get; set; }
    public Store Store { get; set; }
}
