using System.Collections.Generic;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class Function
{
    public GasType ReturnType { get; protected set; }
    public List<GasType> ParameterTypes { get; protected set; }
    public List<Variable> Parameters { get; protected set; }
    public Statement Statements { get; protected set; }
    public Scope Scope { get; set; }

    public Function(GasType returnType, List<Variable> parameters, Statement statements, Scope scope)
    {
        ReturnType = returnType;
        Parameters = parameters;
        Statements = statements;
        Scope = scope;
    }
}
