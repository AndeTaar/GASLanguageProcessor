﻿using String = string;

namespace GASLanguageProcessor.TableType;

public class Function
{
    public Function(List<String> parameters, Statement statements, VarEnv varEnv, FuncEnv funcEnv, Store store, bool isConstructor = false)
    {
        Parameters = parameters;
        Statements = statements;
        VarEnv = varEnv;
        FuncEnv = funcEnv;
        Store = store;
        IsConstructor = isConstructor;
    }

    public List<String> Parameters { get; protected set; }
    public Statement Statements { get; protected set; }
    public bool IsConstructor { get; protected set; }
    public VarEnv VarEnv { get; set; }
    public FuncEnv FuncEnv { get; set; }
    public Store Store { get; set; }
}
