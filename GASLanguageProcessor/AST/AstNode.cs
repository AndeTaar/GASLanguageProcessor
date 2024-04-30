using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST;

public abstract class AstNode
{
    public abstract T Accept<T>(IAstVisitor<T> visitor);
    public Scope? Scope { get; set; }
    public int LineNumber { get; set; }
}
