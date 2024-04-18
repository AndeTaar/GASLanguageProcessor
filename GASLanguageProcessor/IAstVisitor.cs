namespace GASLanguageProcessor.AST;

public interface IAstVisitor
{
    AstNode Visit(AstNode node);
}