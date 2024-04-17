﻿namespace GASLanguageProcessor.AST.Statements;

public class Compound : Statement
{
    public Statement Statement1 { get; protected set; }
    public Statement Statement2 { get; protected set; }

    public Compound(Statement statement1, Statement statement2)
    {
        Statement1 = statement1;
        Statement2 = statement2;
    }
}