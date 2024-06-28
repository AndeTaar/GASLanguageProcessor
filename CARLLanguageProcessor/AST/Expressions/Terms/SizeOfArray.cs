﻿using CARLLanguageProcessor.AST.Expressions.Terms.Identifiers;
using CARLLanguageProcessor.TableType;

namespace CARLLanguageProcessor.AST.Expressions.Terms;

public class SizeOfArray : Term
{
    public SizeOfArray(Identifier listIdentifier)
    {
        ListIdentifier = listIdentifier;
    }

    public Identifier ListIdentifier { get; protected set; }

    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitLengthOfArray(this, envT);
    }
}