﻿using GASLanguageProcessor.AST.Expressions;

namespace GASLanguageProcessor.AST.Terms;

public class Group: AstNode
{
    public Identifier Identifier { get; protected set; }
    public List<AstNode> Terms { get; protected set; }
    public AstNode Point { get; protected set; }

    public Group(Identifier identifier, AstNode point, List<AstNode> terms)
    {
        Identifier = identifier;
        Terms = terms;
        Point = point;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitGroup(this);
    }
}