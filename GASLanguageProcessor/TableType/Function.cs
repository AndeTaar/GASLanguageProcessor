using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class Function
{
    public GasType ReturnType { get; set; }
    public List<Variable> Parameters { get; protected set; }
    public Statement Statements { get; protected set; }
    public Scope Scope { get; set; }

    public Function(List<Variable> parameters, GasType returnType, Statement statements, Scope scope)
    {
        Parameters = parameters;
        Statements = statements;
        Scope = scope;
        ReturnType = returnType;
    }

    public string ParametersToString()
    {
        string result = "";
        int i = 0;
        for (i = 0; i < this.Parameters.Count-1; i++)
        {
            result += i + ": " + this.Parameters[i].Type + " " + this.Parameters[i].Identifier + ", ";
        }
        result += i + ": " + this.Parameters[i].Type + " " + this.Parameters[i].Identifier;

        return result;
    }

    public Function(GasType gasType, List<Variable> parameters, Statement statements, Scope scope)
    {
        Parameters = parameters;
        Statements = statements;
        ReturnType = gasType;
        Scope = scope;
    }
}
