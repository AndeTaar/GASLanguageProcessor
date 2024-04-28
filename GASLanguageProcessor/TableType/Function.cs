using System.Collections.Generic;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class Function
{
    public GasType ReturnType { get; protected set; }

    public List<GasType> ParameterTypes { get; protected set; }
    public List<Variable> Parameters { get; protected set; }
    public Compound Statements { get; protected set; }

    public Function(GasType returnType, List<Variable> parameters, Compound statements)
    {
        ReturnType = returnType;
        Parameters = parameters;
        Statements = statements;
    }
}
