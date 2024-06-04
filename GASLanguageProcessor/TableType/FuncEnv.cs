﻿using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Expressions.Terms.Identifiers;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using String = System.String;

namespace GASLanguageProcessor.TableType;

public class FuncEnv
{
    public Dictionary<string, Function> Functions { get; protected set; } = new();

    public FuncEnv? Parent { get; set; }

    public FuncEnv EnterScope()
    {
        return new FuncEnv(this);
    }

    public FuncEnv ExitScope()
    {
        return Parent ?? this;
    }

    public FuncEnv(FuncEnv parent)
    {
        this.Parent = parent;
    }

    public FuncEnv(Store store, VarEnv? parentEnv = null, FuncEnv? parent = null)
    {
        this.Parent = parent;

        if (parent != null)
        {
            return;
        }

        this.Bind("AddToList", new Function(new List<String>() {"element", "list"},
            new Return(new AddToList(new Identifier("element"),new Identifier("list"))), new VarEnv(parentEnv), this, store));

        this.Bind("RemoveFromList", new Function(new List<String>() {"index", "list"},
            new Return(new RemoveFromList(new Identifier("index"),new Identifier("list"))), new VarEnv(parentEnv), this, store));

        this.Bind("GetFromList", new Function(new List<String>() {"index", "list"},
            new Return(new GetFromList(new Identifier("index"), new Identifier("list"))), new VarEnv(parentEnv), this, store));

        this.Bind("LengthOfList", new Function(new List<String>() {"list"},
            new Return(new LengthOfList(new Identifier("list"))), new VarEnv(parentEnv), this, store));


    }

    public bool Bind(string key, Function value)
    {
        if(Functions.ContainsKey(key))
        {
            return false;
        }
        Functions.Add(key, value);
        return true;
    }

    public Function? LookUp(string key)
    {
        if (Functions.ContainsKey(key))
        {
            return Functions[key];
        }
        return this.Parent?.LookUp(key);
    }
}
