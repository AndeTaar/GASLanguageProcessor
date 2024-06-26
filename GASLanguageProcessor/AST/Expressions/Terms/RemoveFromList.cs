﻿using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class RemoveFromList : Term
{
    public Expression Index { get; protected set; }
    public Identifier ListIdentifier { get; protected set; }
    
    public RemoveFromList(Expression index, Identifier listIdentifier)
    {
        Index = index;
        ListIdentifier = listIdentifier;
    }
    
    public override T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT)
    {
        return visitor.VisitRemoveFromList(this, envT);
    }
}