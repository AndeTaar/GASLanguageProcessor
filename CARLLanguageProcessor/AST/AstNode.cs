﻿using CARLLanguageProcessor.TableType;

namespace CARLLanguageProcessor.AST;

public abstract class AstNode
{
    public int LineNum { get; set; }
    public abstract T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT);
}