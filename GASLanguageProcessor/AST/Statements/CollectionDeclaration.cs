﻿using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.TableType;
using Type = GASLanguageProcessor.AST.Expressions.Terms.Type;

namespace GASLanguageProcessor.AST.Statements;

public class CollectionDeclaration: Statement
{
    public Type Type { get; set; }
    public Identifier Identifier { get; protected set; }
    public Expression? Expression { get; protected set; }

    public CollectionDeclaration(Type type, Identifier identifier, Expression? expression)
    {
        Type = type;
        Identifier = identifier;
        Expression = expression;
    }


    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitCollectionDeclaration(this, scope);
    }
}