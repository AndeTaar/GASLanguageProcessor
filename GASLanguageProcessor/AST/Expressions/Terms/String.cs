﻿using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class String: Term
{
    public string Value { get; protected set; }

    public String(string value)
    {
        Value = value;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitString(this, envT);
    }

}
