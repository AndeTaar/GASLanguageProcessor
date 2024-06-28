namespace CARLLanguageProcessor.AST.Expressions;

public abstract class Expression : AstNode
{
    public string connectedIdentifier { get; set; }
}
