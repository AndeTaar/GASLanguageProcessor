using GASLanguageProcessor.AST;

namespace GASLanguageProcessor;

public class SimpleAstVisitor : IAstVisitor
{
    public AstNode Visit(AstNode node)
    {
        node.Accept(this, "");
        return node;
    }

}