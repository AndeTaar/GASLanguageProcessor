using System.Collections;
using Antlr4.Runtime.Misc;
using GASLanguageProcessor.AST;

namespace GASLanguageProcessor;

public abstract class AstNode
{
    public abstract AstNode Accept(IAstVisitor visitor);
}