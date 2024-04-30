using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class Function
{
    public GasType ReturnType { get; set; }
    public List<Variable> Parameters { get; protected set; }
    public Statement Statements { get; protected set; }
    public Scope Scope { get; set; }

    public Function(List<Variable> parameters, Statement statements, Scope scope)
    {
        Parameters = parameters;
        Statements = statements;
        Scope = scope;
    }

    public Function(GasType gasType, List<Variable> parameters, Statement statements, Scope scope)
    {
        Parameters = parameters;
        Statements = statements;
        ReturnType = gasType;
        Scope = scope;
    }
}
