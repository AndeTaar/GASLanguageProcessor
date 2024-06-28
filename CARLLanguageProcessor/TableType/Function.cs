using String = string;

namespace CARLLanguageProcessor.TableType;

public class Function
{
    public Function(List<String> parameters, Statement statements, VarEnv varEnv, FuncEnv funcEnv, Store store)
    {
        Parameters = parameters;
        Statements = statements;
        VarEnv = varEnv;
        FuncEnv = funcEnv;
        Store = store;
    }

    public List<String> Parameters { get; protected set; }
    public Statement Statements { get; protected set; }
    public VarEnv VarEnv { get; set; }
    public FuncEnv FuncEnv { get; set; }
    public Store Store { get; set; }
}
