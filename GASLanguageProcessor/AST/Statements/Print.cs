namespace GASLanguageProcessor.AST.Statements;

public class Print : Statement
{
    public AstNode Expression { get; protected set; }

    public Print(AstNode expression)
    {
        Expression = expression;
    }

}