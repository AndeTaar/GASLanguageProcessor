using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST;

public abstract class AstNode
{
    public abstract T Accept<T>(IAstVisitor<T> visitor, TypeEnv envT);
    public int LineNum { get; set; }
}
